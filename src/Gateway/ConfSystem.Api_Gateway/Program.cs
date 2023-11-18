var builder = WebApplication.CreateBuilder(args);

// Yarp config
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("reverseProxy"));

var app = builder.Build();
app.MapGet("/", () => "ConfSystem Api Gateway");

app.MapReverseProxy();

app.Run();