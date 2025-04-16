namespace Notification.Api.Extension
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            return services.AddCors(policy =>
            {
                policy.AddPolicy("NotificationPolicy", options =>
                {
                    options.AllowAnyHeader()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
    }
}
