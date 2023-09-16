using System.Runtime.CompilerServices;
using ConfSystem.Modules.Conferences.Core.Policies;
using ConfSystem.Modules.Conferences.Core.Repositories;
using ConfSystem.Modules.Conferences.Core.Repositories.Conference;
using ConfSystem.Modules.Conferences.Core.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Modules.Conferences.Api")]
namespace ConfSystem.Modules.Conferences.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IHostRepository, InMemoryHostRepository>();
        services.AddSingleton<IConferenceRepository, InMemoryConferenceRepository>();
        
        services.AddSingleton<IHostDeletePolicy, HostDeletePolicy>();
        services.AddSingleton<IConferenceDeletionPolicy, ConferenceDeletionPolicy>();
        
        services.AddScoped<IHostService, HostService>();
        services.AddScoped<IConferenceService, ConferenceService>();
        return services;
    }
}