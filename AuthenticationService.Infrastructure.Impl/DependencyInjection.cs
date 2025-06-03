using AuthenticationService.Application.Services;
using AuthenticationService.Infrastructure.Impl.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.Infrastructure.Impl
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddImplementation(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
