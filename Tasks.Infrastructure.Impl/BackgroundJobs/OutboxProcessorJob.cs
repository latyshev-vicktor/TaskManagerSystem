using Dapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Npgsql;
using Quartz;
using System.Text.Json;
using Tasks.Domain.Entities;

namespace Tasks.Infrastructure.Impl.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class OutboxProcessorJob(
        NpgsqlDataSource dataSource, 
        IPublishEndpoint publishEndpoint,
        ILogger<OutboxProcessorJob> logger) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await using var connection = await dataSource.OpenConnectionAsync();
            await using var transaction = await connection.BeginTransactionAsync();

            var messages = (await connection.QueryAsync<OutboxMessageEntity>(
                @"
                SELECT id AS Id, type AS Type, content AS Content
                FROM outboxmessages
                WHERE ProcessedOnUtc IS NULL
                ORDER BY OccurredOnUtc LIMIT 100
                ",
                transaction: transaction)).AsList();


            foreach(var message in messages)
            {
                try
                {
                    var messageType = Type.GetType(message.Type);
                    var deserializedMessage = JsonSerializer.Deserialize(message.Content, messageType!);

                    await publishEndpoint.Publish(deserializedMessage!);

                    await connection.ExecuteAsync(
                        @"
                        UPDATE outboxmessages
                        SET ProcessedOnUtc = @ProcessedOnUtc
                        WHERE Id = @Id
                        ",
                    new { ProcessedOnUtc = DateTime.UtcNow, message.Id },
                    transaction: transaction);


                }
                catch (Exception ex)
                {
                    await connection.ExecuteAsync(
                        @"
                        UPDATE outboxmessages
                        SET ProcessedOnUtc = @ProcessedOnUtc, Error = @Error
                        WHERE Id = @Id
                        ",
                        new { ProcessedOnUtc = DateTime.UtcNow, Error = ex.ToString(), message.Id },
                        transaction: transaction);
                }
            }

            await transaction.CommitAsync();
            await connection.CloseAsync();
        }
    }
}
