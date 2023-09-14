using ConfSystem.Modules.Conferences.Api;
using ConfSystem.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddConferences();


var app = builder.Build();

app.UseInfrastructure();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();