using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManagerSystem.Common.Interfaces;
using TaskManagerSystem.Common.MediatorPipelines;

namespace AuthenticationService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var referencedAssemblies = entryAssembly!
                .GetReferencedAssemblies()
                .Where(x => x.Name.Contains("AuthenticationService.Application"))
                .Select(Assembly.Load);

            var assembliesToScan = new[] { entryAssembly }.Concat(referencedAssemblies).ToArray();

            services.Scan(scan => scan
                .FromAssemblies(assembliesToScan)
                .AddClasses(c => c.AssignableTo(typeof(IRequestValidator<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );


            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                options.AddOpenBehavior(typeof(DistributionLockPipelineBehaviour<,>));
                options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            return services;
        }
    }
}
