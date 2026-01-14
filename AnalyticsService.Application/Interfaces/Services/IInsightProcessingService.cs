using AnalyticsService.Domain.Entities;

namespace AnalyticsService.Application.Interfaces.Services
{
    public interface IInsightProcessingService
    {
        Task Proccess(IReadOnlyList<InsightEntity> insights);
    }
}
