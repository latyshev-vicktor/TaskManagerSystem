using AnalyticsService.Application.Dto;
using AnalyticsService.Application.Interfaces.Detectors;
using AnalyticsService.Domain.Entities;
using AnalyticsService.Domain.Enums;

namespace AnalyticsService.Infrastructure.Impl.Detectors
{
    public class LowCompletionDetector : IInsightDetector<SprintAnalyticsContext>
    {
        private const double THERESHOLD = 0.4;
        private const double THERESHOLD_TOTAL_TASKS = 5;
        public Task<InsightEntity?> Detect(SprintAnalyticsContext context)
        {
            //Мало задач в спринте
            if (context.TotalTasks < THERESHOLD_TOTAL_TASKS)
                return null!;

            if (context.CompletionRate >= THERESHOLD)
                return null!;

            var calculateConfidence = CalculateConfidence(context.CompletionRate);

            return Task.FromResult<InsightEntity?>(
                new InsightEntity(
                    type: InsightType.LowCompletion,
                    severity: Severity.Warning,
                    confidence: calculateConfidence,
                    sprintId: context.SprintId,
                    userId: context.UserId
                )
            );
        }

        private static double CalculateConfidence(double completion)
        {
            return Math.Clamp(
                (THERESHOLD - completion) / THERESHOLD,
                0.3,
                1.0
            );
        }
    }
}
