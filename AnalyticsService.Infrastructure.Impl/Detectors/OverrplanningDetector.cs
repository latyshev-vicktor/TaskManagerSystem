using AnalyticsService.Application.Dto;
using AnalyticsService.Application.Interfaces.Detectors;
using AnalyticsService.Domain.Entities;
using AnalyticsService.Domain.Enums;

namespace AnalyticsService.Infrastructure.Impl.Detectors
{
    public class OverrplanningDetector : IInsightDetector<SprintAnalyticsContext>
    {
        public Task<InsightEntity?> Detect(SprintAnalyticsContext context)
        {
            if (context.TasksAddedAfterStart < 5)
                return Task.FromResult<InsightEntity>(null!)!;

            return Task.FromResult<InsightEntity?>(new InsightEntity(
                InsightType.Overplanning,
                Severity.Warning,
                1,
                context.SprintId,
                context.UserId,
                $"После старта спринта {context.Name} было добавлено {context.TasksAddedAfterStart}. Постарайтесь заводить меньше задач, чтобы успеть с срок"));
        }
    }
}
