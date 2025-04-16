namespace Tasks.Api.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            return services.AddCors(policy =>
            {
                policy.AddPolicy("TaskPolicy", options =>
                {
                    options.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
    }
}
