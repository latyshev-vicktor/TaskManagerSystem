using AnalyticsService.Application.DetectorPipelines;
using AnalyticsService.Application.Dto;
using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using AnalyticsService.Domain.Enums;
using AnalyticsService.Domain.Repositories;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;

namespace AnalyticsService.Infrastructure.Impl.Services
{
    public class TaskStatusChangedService(
        ISprintTaskAnalyticsRepository sprintTaskAnalyticsRepository,
        ISprintAnalitycsRepository sprintAnalitycsRepository,
        ISprintRecalculationService sprintRecalculationService,
        InsightDetectionPipeline detectionPipeline,
        IInsightProcessingService insightProcessingService) : ITaskStatusChangedService
    {
        public async Task Handle(TaskStatusChangedEvent contractMessage, CancellationToken ct)
        {
            var status = TasksStatus.Created;
            var task = await sprintTaskAnalyticsRepository.GetByTask(contractMessage.TaskId);
            if (task == null)
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
            var sprintAnalyticsContext = new SprintAnalyticsContext(savedSprint!.SprintId, savedSprint.UserId, savedSprint.TotalTasks, savedSprint.CompletedTasks, savedSprint.Name);

            var insigns = await detectionPipeline.Deletect(sprintAnalyticsContext);
            await insightProcessingService.Proccess(insigns);

        }
    }
}
