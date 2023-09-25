using System.Runtime.CompilerServices;
using ConfSystem.Modules.Tickets.Core.DAL;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Modules.Tickets.Api")]
namespace ConfSystem.Modules.Tickets.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<TicketsDbContext>();
        return services;
    }
}