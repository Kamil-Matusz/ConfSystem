using System.Reflection;
using ConfSystem.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Shared.Infrastructure.Events;

internal static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        //Scrutor scan assemblies
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}