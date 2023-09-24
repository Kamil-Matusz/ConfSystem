using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using ConfSystem.Modules.Speakers.Core;

[assembly: InternalsVisibleTo("ConfSystem.Bootstrapper")]
namespace ConfSystem.Modules.Speakers.Api;

internal static class Extensions
{
    public static IServiceCollection AddSpeakers(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }
}