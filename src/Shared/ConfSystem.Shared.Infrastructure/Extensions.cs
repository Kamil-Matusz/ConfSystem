using System.Runtime.CompilerServices;
using ConfSystem.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Bootstrapper")]
namespace ConfSystem.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}