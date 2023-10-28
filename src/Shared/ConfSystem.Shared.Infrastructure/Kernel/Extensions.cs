using System.Reflection;
using ConfSystem.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Shared.Infrastructure.Kernel;

internal static class Extensions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        //Scrutor scan assemblies
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        return services;
    }
}