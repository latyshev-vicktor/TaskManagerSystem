using MassTransit;
using Notification.Application.Consumers;

namespace Notification.Api.Extension
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<SendEmailByCreatedNewUserConsumer>();
                x.AddConsumer<SprintChangeStatusConsumer>();

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
