using Dapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Npgsql;
using Quartz;
using System.Collections.Concurrent;
using System.Text.Json;
using Tasks.Domain.Entities;
using Tasks.Infrastructure.Impl.Dto;

namespace Tasks.Infrastructure.Impl.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class OutboxProcessorJob(
        NpgsqlDataSource dataSource, 
        IPublishEndpoint publishEndpoint,
        ILogger<OutboxProcessorJob> logger) : IJob
    {
        private const int BATCH_SIZE = 1000;
        private static readonly ConcurrentDictionary<string, Type> MessageTypes = [];

        private static Type GetOrAddType(string typeName)
        {
            return MessageTypes.GetOrAdd(typeName, name => Type.GetType(name)!);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var maxDegreeOfParallelism = Environment.ProcessorCount * 2;
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism,
                CancellationToken = context.CancellationToken,
            };

            await Parallel.ForEachAsync(
                Enumerable.Range(0, maxDegreeOfParallelism),
                parallelOptions,
                async (_, token) =>
                {
                    await Proccess(token);
                });
        }

        private async Task Proccess(CancellationToken cancellationToken)
        {
            var updatedMessageQueue = new ConcurrentQueue<OutboxUpdate>();

            await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);
            await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

            var messages = (await connection.QueryAsync<OutboxMessageEntity>(
                @"
                SELECT id AS Id, type AS Type, content AS Content
                FROM outboxmessages
                WHERE ProcessedOnUtc IS NULL
                ORDER BY OccurredOnUtc LIMIT @BATCH_SIZE
                FOR UPDATE SKIP LOCKED
                ",
                new { BATCH_SIZE },
                transaction: transaction)).AsList();

            if (messages.Count == 0)
            {
                return;
            }

            var publishTasks = messages.Select(message => PublishMessage(message, updatedMessageQueue, cancellationToken));

            await Task.WhenAll(publishTasks);

            await UpdateMessages(updatedMessageQueue, connection, transaction);

            await transaction.CommitAsync(cancellationToken);
            await connection.CloseAsync();
        }

        private static async Task UpdateMessages(
            ConcurrentQueue<OutboxUpdate> updatedMessages, 
            NpgsqlConnection connection,
            NpgsqlTransaction transaction)
        {
            var updateSqlQuery =
                @"
                UPDATE outboxmessages
                SET ProcessedOnUtc = v.ProcessedOnUtc,
                    Error = v.Error
                FROM (VALUES
                    {0}
                ) AS v(Id, ProcessedOnUtc, Error)
                WHERE outboxmessages.Id = v.Id::uuid
                ";

            var messages = updatedMessages.ToList();
            var paramNames = string.Join(",", messages.Select((_, i) => $"(@Id{i}, @ProcessedOn{i}, @Error{i})"));
            var formattedSql = string.Format(updateSqlQuery, paramNames);

            var parameters = new DynamicParameters();

            for (int i = 0; i < messages.Count; i++)
            {
                parameters.Add($"Id{i}", messages[i].Id.ToString());
                parameters.Add($"ProcessedOn{i}", messages[i].ProcessedOnUtc);
                parameters.Add($"Error{i}", messages[i].Error);
            }

            await connection.ExecuteAsync(formattedSql, parameters, transaction: transaction);
        }

        private async Task PublishMessage(
            OutboxMessageEntity message, 
            ConcurrentQueue<OutboxUpdate> updatedMessages, 
            CancellationToken cancellationToken)
        {
            try
            {
                var messageType = GetOrAddType(message.Type);
                var deserializedMessage = JsonSerializer.Deserialize(message.Content, messageType!);

                await publishEndpoint.Publish(deserializedMessage!, cancellationToken);

                updatedMessages.Enqueue(new OutboxUpdate
                {
                    Id = message.Id,
                    ProcessedOnUtc = DateTime.UtcNow,
                });
            }
            catch (Exception ex)
            {
                updatedMessages.Enqueue(new OutboxUpdate
                {
                    Id = message.Id,
                    ProcessedOnUtc = DateTime.UtcNow,
                    Error = ex.ToString()
                });
            }

        }
    }
}
