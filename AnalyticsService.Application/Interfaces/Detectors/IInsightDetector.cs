using AnalyticsService.Application.Dto;
using AnalyticsService.Domain.Entities;

namespace AnalyticsService.Application.Interfaces.Detectors
{
    public interface IInsightDetector<in TContext>
        where TContext : SprintAnalyticsContext, new()
    {
        Task<InsightEntity?> Detect(TContext context);
    }
}
