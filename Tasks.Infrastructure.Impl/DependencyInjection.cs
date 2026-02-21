using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Quartz;
using Tasks.Application.Services;
using Tasks.Infrastructure.Impl.BackgroundJobs;
using Tasks.Infrastructure.Impl.Services;

namespace Tasks.Infrastructure.Impl
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(config =>
            {
                var connectionString = configuration.GetConnectionString("DbConnection")
                    ?? throw new InvalidOperationException("Не найдена строка подключения к БД сервиса Tasks");

                var builder = new NpgsqlDataSourceBuilder(connectionString);

                return builder.Build();
            });

            services.AddScoped<IOutboxMessageService, OutboxMessageService>();

            services.AddQuartz(config =>
            {
                var jobKey = new JobKey(nameof(OutboxProcessorJob));
                config.AddJob<OutboxProcessorJob>(opts => opts.WithIdentity(jobKey));

                config.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("OutboxProcessorJob-trigger")
                    .WithCronSchedule("0/5 * * * * ?")
                );
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            return services;
        }
    }
}
