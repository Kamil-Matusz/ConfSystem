using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Modules.Conferences.Api")]
namespace ConfSystem.Modules.Conferences.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}