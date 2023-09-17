using System.Runtime.CompilerServices;
using ConfSystem.Shared.Abstractions;
using ConfSystem.Shared.Infrastructure.Api;
using ConfSystem.Shared.Infrastructure.Errors;
using ConfSystem.Shared.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Bootstrapper")]
namespace ConfSystem.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddErrorHandling();
        services.AddSingleton<IClock, Clock>();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        return app;
    }
}