using System.Runtime.CompilerServices;
using ConfSystem.Modules.Attendances.Application;
using ConfSystem.Modules.Attendances.Domain;
using ConfSystem.Modules.Attendances.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Bootstrapper")]
namespace ConfSystem.Modules.Attendances.Api;

internal static class Extensions
{
    public static IServiceCollection AddAAttendances(this IServiceCollection services)
    {
        services.AddDomain();
        services.AddApplication();
        services.AddInfrastructure();
        return services;
    }
}