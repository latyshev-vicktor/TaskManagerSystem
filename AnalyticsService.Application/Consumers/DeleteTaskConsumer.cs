using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities;
using AnalyticsService.Domain.Repositories;
using DnsClient.Internal;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;

namespace AnalyticsService.Application.Consumers
{
    public class DeleteTaskConsumer(
        ISprintTaskAnalyticsRepository sprintTaskAnalyticsRepository,
        ISprintAnalitycsRepository sprintAnalitycsRepository,
        AnalyticsDbContext dbContext,
        ILogger<DeleteTaskConsumer> logger,
        ITaskQueueService taskQueueService) : IConsumer<DeleteTaskEvent>
    {
        public async Task Consume(ConsumeContext<DeleteTaskEvent> context)
        {
            var contractMessage = context.Message;
            var key = $"sprintId-{contractMessage.SprintId}";

            await taskQueueService.Execute(key, async () =>
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync(context.CancellationToken);

                try
                {
                    var existEvent = await dbContext.MessageConsumers.AnyAsync(
                        x => x.EventId == contractMessage.EventId
                        && x.ConsumerName == nameof(DeleteTaskConsumer),
                        cancellationToken: context.CancellationToken);

                    if (existEvent)
                    {
                        logger.LogInformation("Событие DeleteTaskConsumer с EventId [{eventId}] уже обработано", context.Message.EventId);
                        return;
                    }

                    var task = await sprintTaskAnalyticsRepository.GetByTask(contractMessage.TaskId)
                        ?? throw new ApplicationException($"Не найдена задача по Id {contractMessage.TaskId}");

                    var linkagesSprint = await sprintAnalitycsRepository.GetBySprintId(task.SprintId)
                        ?? throw new ApplicationException($"Не найден спринт, привязанный к задаче с Id {task.TaskId}");

                    await sprintTaskAnalyticsRepository.Delete(contractMessage.TaskId, context.CancellationToken);

                    await dbContext.MessageConsumers.AddAsync(new MessageConsumerEntity
                    {
                        EventId = contractMessage.EventId,
                        ConsumerName = nameof(DeleteTaskConsumer),
                        ConsumedAtUtc = DateTime.UtcNow,
                    },
                    cancellationToken: context.CancellationToken);

                    await dbContext.SaveChangesAsync(context.CancellationToken);
                    await transaction.CommitAsync();
                }
                catch (DbUpdateException)
                {
                    logger.LogInformation("Событие DeleteTaskConsumer с EventId [{eventId}] уже обработано", context.Message.EventId);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    logger.LogError(ex, "Ошибка при обработке DeleteTaskConsumer с EventId [{eventId}]", contractMessage.EventId);
                }
            });
        }
    }
}
