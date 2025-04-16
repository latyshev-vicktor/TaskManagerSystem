namespace AuthenticationService.Api.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            return services.AddCors(policy =>
            {
                policy.AddPolicy("AuthenticationPolicy", options =>
                {
                    options.AllowAnyHeader()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
    }
}
