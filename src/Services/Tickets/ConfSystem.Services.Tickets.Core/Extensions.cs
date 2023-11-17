using System.Runtime.CompilerServices;
using ConfSystem.Services.Tickets.Core.DAL;
using ConfSystem.Services.Tickets.Core.DAL.Repositories.Conferences;
using ConfSystem.Services.Tickets.Core.DAL.Repositories.Tickets;
using ConfSystem.Services.Tickets.Core.DAL.Repositories.TicketsSale;
using ConfSystem.Services.Tickets.Core.Services.Conferences;
using ConfSystem.Services.Tickets.Core.Services.Tickets;
using ConfSystem.Services.Tickets.Core.Services.TicketsSale;
using ConfSystem.Shared.Abstractions;
using ConfSystem.Shared.Infrastructure.Auth;
using ConfSystem.Shared.Infrastructure.Commands;
using ConfSystem.Shared.Infrastructure.Contexts;
using ConfSystem.Shared.Infrastructure.Errors;
using ConfSystem.Shared.Infrastructure.Events;
using ConfSystem.Shared.Infrastructure.Kernel;
using ConfSystem.Shared.Infrastructure.Messaging;
using ConfSystem.Shared.Infrastructure.Modules;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using ConfSystem.Shared.Infrastructure.Queries;
using ConfSystem.Shared.Infrastructure.Services;
using Convey;
using Convey.CQRS.Events;
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Services.Tickets.Api")]
namespace ConfSystem.Services.Tickets.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        services.AddErrorHandling();
        services.AddSingleton<IContextFactory, ContextFactory>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
        
        services.AddSingleton<IClock, Clock>();
        
        // events registration
        services.AddModuleRequest(assemblies);
        services.AddEvents(assemblies);
        services.AddDomainEvents(assemblies);
        services.AddMessaging();
        
        // decorators
        services.AddPostgres();
        services.AddTransactionalDecorators();

        // CQRS
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        
        // auth registration
        services.AddAuth();
        
        services.AddHostedService<DatabaseInitializer>();
        services.AddControllers();
        
        // RabbitMq
        services
            .AddConvey()
            .AddRabbitMq()
            .AddEventHandlers()
            .AddInMemoryEventDispatcher()
            .Build();
        
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