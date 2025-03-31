using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Services;
using Notification.Infrastructure.Impl.Services;

namespace Notification.Infrastructure.Impl
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
