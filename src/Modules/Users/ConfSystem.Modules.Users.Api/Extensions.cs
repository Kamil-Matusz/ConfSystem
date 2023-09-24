using System.Runtime.CompilerServices;
using ConfSystem.Modules.Users.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Bootstrapper")]
namespace ConfSystem.Modules.Users.Api;

internal static class Extensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }
}