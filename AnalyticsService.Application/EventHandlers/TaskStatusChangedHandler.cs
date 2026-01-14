using MassTransit;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
using AnalyticsService.Domain.Repositories;
using AnalyticsService.Domain.Enums;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.Application.DetectorPipelines;
using AnalyticsService.Application.Dto;

namespace AnalyticsService.Application.EventHandlers
{
    public class TaskStatusChangedHandler(
        ISprintTaskAnalyticsRepository sprintTaskAnalyticsRepository,
        ISprintAnalitycsRepository sprintAnalitycsRepository,
        ISprintRecalculationService sprintRecalculationService,
        InsightDetectionPipeline detectionPipeline) : IConsumer<TaskStatusChangedEvent>
    {
        public async Task Consume(ConsumeContext<TaskStatusChangedEvent> context)
        {
            var contractMessage = context.Message;
            var status = TasksStatus.Created;
            var task = await sprintTaskAnalyticsRepository.GetByTask(contractMessage.TaskId);
            if(task == null)
            {
                task = new SprintTaskAnalyticsEntity(contractMessage.SprintId, contractMessage.TaskId);
                await sprintTaskAnalyticsRepository.Add(task);
            }
            else
            {
                task.UpdateStatus(status);
                await sprintTaskAnalyticsRepository.Save(task);
            }

            await sprintRecalculationService.RecalculateSprint(contractMessage.SprintId, contractMessage.UserId);

            var savedSprint = await sprintAnalitycsRepository.GetBySprintId(contractMessage.SprintId);

            var sprintAnalyticsContext = new SprintAnalyticsContext(savedSprint.SprintId, savedSprint.UserId, savedSprint.TotalTasks, savedSprint.CompletedTasks, savedSprint.Name);

            await detectionPipeline.Deletect(sprintAnalyticsContext);
        }
    }
}
