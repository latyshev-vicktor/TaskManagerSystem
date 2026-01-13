using AnalyticsService.Application.DetectorPipelines;
using AnalyticsService.Application.Dto;
using AnalyticsService.Application.Interfaces.Detectors;
using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.Domain.Repositories;
using AnalyticsService.Infrastructure.Impl.Detectors;
using AnalyticsService.Infrastructure.Impl.Repositories;
using AnalyticsService.Infrastructure.Impl.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Infrastructure.Impl
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISprintTaskAnalyticsRepository, SprintTaskAnalyticsRepository>();
            services.AddScoped<ISprintAnalitycsRepository, SprintAnalitycsRepository>();
            services.AddScoped<ISprintRecalculationService, SprintRecalculationService>();

            services.AddScoped<IInsightDetector<SprintAnalyticsContext>, LowCompletionDetector>();
            services.AddScoped<InsightDetectionPipeline>();

            return services;
        }
    }
}
