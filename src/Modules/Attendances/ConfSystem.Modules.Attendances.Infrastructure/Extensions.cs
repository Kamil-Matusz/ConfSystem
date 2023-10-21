using ConfSystem.Modules.Attendances.Infrastructure.DAL;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Attendances.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<AttendancesDbContext>();
        return services;
    } 
}