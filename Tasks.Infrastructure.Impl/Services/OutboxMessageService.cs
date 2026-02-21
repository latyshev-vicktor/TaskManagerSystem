using Npgsql;
using System.Text.Json;
using Tasks.Application.Services;
using Tasks.Domain.Entities;
using Dapper;

namespace Tasks.Infrastructure.Impl.Services
{
    public class OutboxMessageService(NpgsqlDataSource dataSource) : IOutboxMessageService
    {
        public async Task Add<T>(T message)
        {
            var outboxMessage = new OutboxMessageEntity
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = typeof(T).AssemblyQualifiedName!,
                Content = JsonSerializer.Serialize(message)
            };

            await using var connection = await dataSource.OpenConnectionAsync();

            await connection.ExecuteAsync(
                    @"
                    INSERT INTO outboxmessages (Id, OccurredOnUtc, Type, Content)
                    VALUES (@Id, @OccurredOnUtc, @Type, @Content::jsonb)
                    ",
                    outboxMessage);

            await connection.CloseAsync();
        }
    }
}
