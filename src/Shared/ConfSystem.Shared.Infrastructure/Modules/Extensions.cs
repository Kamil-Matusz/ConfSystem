using System.Reflection;
using ConfSystem.Shared.Abstractions.Events;
using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Infrastructure.Modules.ModuleSerializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConfSystem.Shared.Infrastructure.Modules;

public static class Extensions
{

    internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
    {
        var moduleInfoProvider = new ModuleInfoProvider();
        var moduleInfo =
            modules?.Select(x => new ModuleInfo(x.Name, x.Path, x.Policies ?? Enumerable.Empty<string>())) ??
            Enumerable.Empty<ModuleInfo>();
        moduleInfoProvider.Modules.AddRange(moduleInfo);
        services.AddSingleton(moduleInfoProvider);

        return services;
    }

    internal static IServiceCollection AddModuleRequest(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddModuleRegistry(assemblies);
        services.AddSingleton<IModuleClient, ModuleClient>();
        services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();
        services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();
        return services;
    }
    
    internal static void MapModuleInfo(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("modules", context =>
        {
            var moduleInfoProvicer = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
            return context.Response.WriteAsJsonAsync(moduleInfoProvicer.Modules);
        });
    }

    public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
        => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();
    
    public static IHostBuilder ConfigureModules(this IHostBuilder builder)
        => builder.ConfigureAppConfiguration((ctx, cfg) =>
        {
            foreach (var settings in GetSettings("*"))
            {
                cfg.AddJsonFile(settings);
            }
            
            foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}"))
            {
                cfg.AddJsonFile(settings);
            }
            
            IEnumerable<string> GetSettings(string pattern) => Directory.EnumerateFiles(
                ctx.HostingEnvironment.ContentRootPath, $"module.{pattern}.json", SearchOption.AllDirectories);
        });

    private static void AddModuleRegistry(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var registry = new ModuleRegistry();
        var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();
        
        var eventTypes = types
            .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
            .ToArray();

        services.AddSingleton<IModuleRegistry>(sp =>
        {
            var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
            var eventDispatcherType = eventDispatcher.GetType();
            
            foreach (var type in eventTypes)
            {
                registry.AddBroadcastAction(type, @event =>
                    (Task) eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))
                        ?.MakeGenericMethod(type)
                        .Invoke(eventDispatcher, new[] {@event}));
            }

            return registry;
        });
    }
}