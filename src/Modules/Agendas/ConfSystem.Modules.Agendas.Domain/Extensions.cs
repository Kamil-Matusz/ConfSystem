using ConfSystem.Modules.Agendas.Domain.Agendas.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Agendas.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IAgendaTracksDomainService, AgendaTracksDomainService>();
        return services;
    } 
}