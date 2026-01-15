using AnalyticsService.Application.Consumers;
using MassTransit;

namespace AnalyticsService.Api.Extensions
{
    public static class MassTransitExtension
    {
        public static IServiceCollection AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<SprintCreatedConsumer>();
                x.AddConsumer<SprintUpdateNameConsumer>();
                x.AddConsumer<TaskStatusChangedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
