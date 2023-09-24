using ConfSystem.Modules.Conferences.Api;
using ConfSystem.Modules.Speakers.Api;
using ConfSystem.Modules.Users.Api;
using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Infrastructure;
using ConfSystem.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

IList<IModule> _modules = null;

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureModules();

builder.Services
    .AddInfrastructure(_modules)
    .AddConferences()
    .AddSpeakers()
    .AddUsers();

var app = builder.Build();

app.UseInfrastructure();

app.MapGet("/", () => "Hello World!");

app.MapModuleInfo();

app.MapControllers();

app.Run();
