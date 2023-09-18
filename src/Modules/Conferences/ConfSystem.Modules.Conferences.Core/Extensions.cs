using System.Runtime.CompilerServices;
using ConfSystem.Modules.Conferences.Core.DAL;
using ConfSystem.Modules.Conferences.Core.DAL.Repositories;
using ConfSystem.Modules.Conferences.Core.Policies;
using ConfSystem.Modules.Conferences.Core.Repositories;
using ConfSystem.Modules.Conferences.Core.Repositories.Conference;
using ConfSystem.Modules.Conferences.Core.Services;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Modules.Conferences.Api")]
namespace ConfSystem.Modules.Conferences.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<ConferencesDbContext>();
       // services.AddSingleton<IHostRepository, InMemoryHostRepository>();
       // services.AddSingleton<IConferenceRepository, InMemoryConferenceRepository>();
        
        services.AddScoped<IHostRepository, HostRepository>();
        services.AddScoped<IConferenceRepository, ConferenceRepository>();
        
        services.AddSingleton<IHostDeletePolicy, HostDeletePolicy>();
        services.AddSingleton<IConferenceDeletionPolicy, ConferenceDeletionPolicy>();
        
        services.AddScoped<IHostService, HostService>();
        services.AddScoped<IConferenceService, ConferenceService>();
        return services;
    }
}