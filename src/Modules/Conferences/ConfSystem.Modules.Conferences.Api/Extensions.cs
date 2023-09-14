using System.Runtime.CompilerServices;
using ConfSystem.Modules.Conferences.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Bootstrapper")]
namespace ConfSystem.Modules.Conferences.Api;

internal static class Extensions
{
    public static IServiceCollection AddConferences(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }
}