using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.Application.UseCases.Sprints.Commands;
using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities;
using DnsClient.Internal;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;

namespace AnalyticsService.Application.Consumers
{
    public class SprintCreatedConsumer(
        ITaskQueueService taskQueueService,
        IMediator mediator,
        AnalyticsDbContext dbContext,
        ILogger<SprintCreatedConsumer> logger) : IConsumer<CreatedNewSprint>
    {
        public async Task Consume(ConsumeContext<CreatedNewSprint> context)
        {
            var contractMessage = context.Message;
            var key = $"sprintId-{contractMessage.SprintId}";
            var cancellationToken = context.CancellationToken;

            logger.LogInformation($"Начала обработки сообщения в {nameof(SprintCreatedConsumer)} с EventId {contractMessage.EventId}");

            await taskQueueService.Execute(key, async () =>
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var existEvent = await dbContext
                        .MessageConsumers
                        .AnyAsync(x => x.EventId == contractMessage.EventId
                                    && x.ConsumerName == nameof(SprintCreatedConsumer));

                    if (existEvent)
                    {
                        return;
                    }

                    var command = new CreateSprintAnalyticsCommand(contractMessage.SprintId, contractMessage.UserId, contractMessage.Name);
                    await mediator.Send(command, cancellationToken);

                    await dbContext.MessageConsumers.AddAsync(new MessageConsumerEntity
                    {
                        EventId = contractMessage.EventId,
                        ConsumedAtUtc = DateTime.UtcNow,
                        ConsumerName = nameof(SprintCreatedConsumer)
                    });

                    await dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (DbUpdateException)
                {
                    //TODO: игнорируем так как это дубль
                    logger.LogInformation($"Дуликат по EventId {contractMessage.EventId}");
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    logger.LogError(ex, $"Ошибка команды {nameof(SprintCreatedConsumer)} с EventId {contractMessage.EventId}");
                    throw;
                }
            });
        }
    }
}
