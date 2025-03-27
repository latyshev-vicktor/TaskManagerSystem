using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.DataAccess.Postgres
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection")
                ?? throw new InvalidOperationException("Не найдена строка подключения к БД сервиса Авторизации");

            services.AddDbContext<AuthenticationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
