using System.Reflection;
using ConfSystem.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Shared.Infrastructure.Events;

internal static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        //Scrutor scan assemblies
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        return services;
    }
}