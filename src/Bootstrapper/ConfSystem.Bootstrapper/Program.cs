using System.Reflection;
using ConfSystem.Bootstrapper;
using ConfSystem.Modules.Agendas.Api;
using ConfSystem.Modules.Attendances.Api;
using ConfSystem.Modules.Conferences.Api;
using ConfSystem.Modules.Saga;
using ConfSystem.Modules.Speakers.Api;
using ConfSystem.Modules.Tickets.Api;
using ConfSystem.Modules.Users.Api;
using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Infrastructure;
using ConfSystem.Shared.Infrastructure.Modules;
using Convey;
using Convey.MessageBrokers.RabbitMQ;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        IConfiguration configuration = builder.Configuration;
        IList<Assembly> _assemblies = AssembliesLoader.LoadAssemblies(configuration);
        IList<IModule> _modules = AssembliesLoader.LoadModules(_assemblies);

        IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureModules();

        builder.Services
            .AddInfrastructure(_modules, _assemblies)
            .AddConferences()
            .AddSpeakers()
            .AddUsers()
            .AddTickets()
            .AddAgendas()
            .AddAAttendances()
            .AddSaga();

        var app = builder.Build();

        app.UseInfrastructure();

        app.MapGet("/", () => "Hello World!");

        app.MapModuleInfo();

// Implemented RabbitMq
app.UseConvey();
app.UseRabbitMq();

app.MapControllers();

        app.Run();

        _assemblies.Clear();
        _modules.Clear();
    }
}
