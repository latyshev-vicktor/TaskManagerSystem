using AnalyticsService.Application.DetectorPipelines;
using AnalyticsService.Application.Dto;
using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using AnalyticsService.Domain.Enums;
using AnalyticsService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;

namespace AnalyticsService.Infrastructure.Impl.Services
{
    public class TaskStatusChangedService(
        ISprintTaskAnalyticsRepository sprintTaskAnalyticsRepository,
        ISprintAnalitycsRepository sprintAnalitycsRepository,
        InsightDetectionPipeline detectionPipeline,
        IInsightProcessingService insightProcessingService,
        AnalyticsDbContext dbContext) : ITaskStatusChangedService
    {
        public async Task Handle(TaskStatusChangedEvent contractMessage, CancellationToken ct)
        {
            if (!Enum.TryParse<TasksStatus>(contractMessage.Status, out var status))
            {
                throw new InvalidOperationException($"Не удалось распарсить строку contract.Message {contractMessage.Status} к enum {typeof(TasksStatus).Name}");
            }

            var newStatus = status;
            var sprintId = contractMessage.SprintId;
            var taskId = contractMessage.TaskId;

            var task = await dbContext.SprintTaskAnalytics.FirstOrDefaultAsync(x => x.TaskId == contractMessage.TaskId, cancellationToken: ct);
            if (task == null)
            {
                task = new SprintTaskAnalyticsEntity(contractMessage.SprintId, contractMessage.TaskId);
                await dbContext.SprintTaskAnalytics.AddAsync(task, ct);
            }
            else
            {
                task.UpdateStatus(newStatus);
            }

            await dbContext.SaveChangesAsync(ct);

            var sprintProjection = await dbContext
                 .SprintAnalitycs
                 .Where(x => x.SprintId == contractMessage.SprintId)
                 .Select(x => new
                 {
                     x.SprintId,
                     x.Name,
                     x.UserId,
                     TotalTaskCount = x.Tasks.Count,
                     CompletedTaskCount = x.Tasks.Where(x => x.Status == TasksStatus.Completed).Count(),
                 }).FirstOrDefaultAsync(ct);

            var sprintAnalyticsContext = new SprintAnalyticsContext(
                sprintProjection!.SprintId, 
                sprintProjection.UserId,
                sprintProjection.TotalTaskCount,
                sprintProjection.CompletedTaskCount,
                sprintProjection.Name);

            var insigns = await detectionPipeline.Deletect(sprintAnalyticsContext);
            await insightProcessingService.Proccess(insigns);
        }
    }
}
