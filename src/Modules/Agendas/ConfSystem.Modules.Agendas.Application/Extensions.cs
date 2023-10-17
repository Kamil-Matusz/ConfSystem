using ConfSystem.Modules.Agendas.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Agendas.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IEventMapper, EventMapper>();
        return services;
    } 
}