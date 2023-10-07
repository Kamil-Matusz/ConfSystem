using System.Runtime.CompilerServices;
using ConfSystem.Modules.Tickets.Core.DAL;
using ConfSystem.Modules.Tickets.Core.DAL.Repositories.Conferences;
using ConfSystem.Modules.Tickets.Core.DAL.Repositories.Tickets;
using ConfSystem.Modules.Tickets.Core.DAL.Repositories.TicketsSale;
using ConfSystem.Modules.Tickets.Core.Services.Conferences;
using ConfSystem.Modules.Tickets.Core.Services.Tickets;
using ConfSystem.Modules.Tickets.Core.Services.TicketsSale;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Modules.Tickets.Api")]
namespace ConfSystem.Modules.Tickets.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<TicketsDbContext>();
        services.AddScoped<IConferenceRepository, ConferenceRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITicketSaleRepository, TicketSaleRepository>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ITicketSaleService, TicketSaleService>();
        services.AddSingleton<ITicketGenerator, TicketGenerator>();
        return services;
    }
}