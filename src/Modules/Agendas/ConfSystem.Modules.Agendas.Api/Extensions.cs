using System.Runtime.CompilerServices;
using ConfSystem.Modules.Agendas.Application;
using ConfSystem.Modules.Agendas.Domain;
using ConfSystem.Modules.Agendas.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Bootstrapper")]
namespace ConfSystem.Modules.Agendas.Api;

internal static class Extensions
{
    public static IServiceCollection AddAgendas(this IServiceCollection services)
    {
        services.AddDomain();
        services.AddApplication();
        services.AddInfrastructure();
        return services;
    } 
}