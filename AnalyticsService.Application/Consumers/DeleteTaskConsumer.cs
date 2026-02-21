using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.Domain.Repositories;
using MassTransit;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;

namespace AnalyticsService.Application.Consumers
{
    public class DeleteTaskConsumer(
        ISprintTaskAnalyticsRepository sprintTaskAnalyticsRepository,
        ISprintAnalitycsRepository sprintAnalitycsRepository,
        ISprintRecalculationService sprintRecalculationService) : IConsumer<DeleteTaskEvent>
    {
        public async Task Consume(ConsumeContext<DeleteTaskEvent> context)
        {
            var contractMessage = context.Message;

            var task = await sprintTaskAnalyticsRepository.GetByTask(contractMessage.TaskId) 
                ?? throw new ApplicationException($"Не найдена задача по Id {contractMessage.TaskId}");

            var linkagesSprint = await sprintAnalitycsRepository.GetBySprintId(task.SprintId) 
                ?? throw new ApplicationException($"Не найден спринт, привязанный к задаче с Id {task.TaskId}");

            await sprintTaskAnalyticsRepository.Delete(contractMessage.TaskId, context.CancellationToken);
            await sprintRecalculationService.RecalculateSprint(linkagesSprint.SprintId, linkagesSprint.UserId);
        }
    }
}
