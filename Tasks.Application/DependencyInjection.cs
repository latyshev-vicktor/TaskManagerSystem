using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using TaskManagerSystem.Common.Interfaces;
using TaskManagerSystem.Common.MediatorPipelines;

namespace Tasks.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

            services.Scan(
            x => {
                var entryAssembly = Assembly.GetEntryAssembly();
                var referencedAssemblies = entryAssembly.GetReferencedAssemblies().Where(x => x.Name.Contains("Tasks.Application")).Select(Assembly.Load);
                var assemblies = new List<Assembly> { entryAssembly }.Concat(referencedAssemblies);
                x.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IRequestValidator<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            return services;
        }
    }
}
