using System.Reflection;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Queries;
using ConfSystem.Shared.Infrastructure.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Shared.Infrastructure.Queries;

internal static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}