using ConfSystem.Modules.Attendances.Domain.Repositories;
using ConfSystem.Modules.Attendances.Infrastructure.DAL;
using ConfSystem.Modules.Attendances.Infrastructure.DAL.Repositories;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Attendances.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<AttendancesDbContext>();
        services.AddScoped<IAttendableEventsRepository, AttendableEventsRepository>();
        services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
        return services;
    } 
}