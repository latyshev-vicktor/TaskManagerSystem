using MassTransit;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
using AnalyticsService.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using AnalyticsService.DataAccess.Postgres;
using Microsoft.Extensions.Logging;
using AnalyticsService.Domain.Entities;

namespace AnalyticsService.Application.Consumers
{
    public class TaskStatusChangedConsumer(
        ITaskStatusChangedService taskStatusChangedService,
        AnalyticsDbContext dbContext,
        ILogger<TaskStatusChangedConsumer> logger,
        ITaskQueueService taskQueueService) : IConsumer<TaskStatusChangedEvent>
    {
        public async Task Consume(ConsumeContext<TaskStatusChangedEvent> context)
        {
            var key = $"sprintId-{context.Message.SprintId}";
            await taskQueueService.Execute(key, async () =>
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync(context.CancellationToken);
                try
                {
                    var contractMessage = context.Message;

                    var existEvent = await dbContext.MessageConsumers.AnyAsync(
                        x => x.EventId == contractMessage.EventId
                        && x.ConsumerName == nameof(TaskStatusChangedConsumer),
                        context.CancellationToken);

                    if (existEvent)
                    {
                        logger.LogInformation("Событие TaskStatusChangedConsumer с EventId [{eventId}] уже обработано", context.Message.EventId);
                        return;
                    }

                    await taskStatusChangedService.Handle(contractMessage, context.CancellationToken);

                    await dbContext.MessageConsumers.AddAsync(new MessageConsumerEntity
                    {
                        EventId = contractMessage.EventId,
                        ConsumerName = nameof(TaskStatusChangedConsumer),
                        ConsumedAtUtc = DateTime.UtcNow,
                    }, cancellationToken: context.CancellationToken);

                    await dbContext.SaveChangesAsync(context.CancellationToken);
                    await transaction.CommitAsync();
                }
                catch (DbUpdateException ex)
                {
                    logger.LogInformation("Событие TaskStatusChangedConsumer с EventId [{eventId}] уже обработано", context.Message.EventId);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    logger.LogError(ex, "Ошибка при обработке команды TaskStatusChangedConsumer по EventId [{eventId}]", context.Message.EventId);
                    throw;
                }
            });
        }
    }
}
