using ConfSystem.Modules.Conferences.Api;
using ConfSystem.Modules.Speakers.Api;
using ConfSystem.Shared.Infrastructure;
using ConfSystem.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);


IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureModules();

builder.Services
    .AddInfrastructure()
    .AddConferences()
    .AddSpeakers();

var app = builder.Build();

app.UseInfrastructure();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
