using ConfSystem.Services.Tickets.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();