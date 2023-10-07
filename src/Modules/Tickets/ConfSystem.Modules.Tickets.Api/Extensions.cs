using System.Runtime.CompilerServices;
using ConfSystem.Modules.Tickets.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Bootstrapper")]
namespace ConfSystem.Modules.Tickets.Api;

internal static class Extensions
{
    public static IServiceCollection AddTickets(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }
}