using AnalyticsService.Application.DetectorPipelines;
using AnalyticsService.Application.Dto;
using AnalyticsService.Application.Interfaces.Detectors;
using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.Infrastructure.Impl.Detectors;
using AnalyticsService.Infrastructure.Impl.Services;
using Medallion.Threading;
using Medallion.Threading.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Infrastructure.Impl
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection")
                ?? throw new InvalidOperationException("Не указана строка подключения к БД сервиса AnalyticsService");

            services.AddScoped<ITaskStatusChangedService, TaskStatusChangedService>();
            services.AddScoped<IInsightProcessingService, InsightProcessingService>();

            services.AddScoped<IInsightDetector<SprintAnalyticsContext>, LowCompletionDetector>();
            services.AddScoped<InsightDetectionPipeline>();

            services.AddSingleton<IDistributedLockProvider>(provider => new PostgresDistributedSynchronizationProvider(connectionString));
            services.AddSingleton<ITaskQueueService, TaskQueueService>();

            return services;
        }
    }
}
