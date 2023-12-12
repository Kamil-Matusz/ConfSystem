using System.Reflection;
using System.Runtime.CompilerServices;
using ConfSystem.Shared.Abstractions;
using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Infrastructure.Api;
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
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

[assembly: InternalsVisibleTo("ConfSystem.Bootstrapper")]
[assembly: InternalsVisibleTo("ConfSystem.Shared.Tests")]
[assembly: InternalsVisibleTo("ConfSystem.Services.Tickets.Core")]
namespace ConfSystem.Shared.Infrastructure;

internal static class Extensions
{
    private const string CorsPolicy = "cors";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IList<IModule> modules, IList<Assembly> assemblies)
    {
        // modules list
        var disabledModules = new List<string>();
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (!key.Contains(":module:enabled"))
                {
                    continue;
                }

                if (!bool.Parse(value))
                {
                    disabledModules.Add(key.Split(":")[0]);
                }
            }
        }
        // CORS Policy
        services.AddCors(cors =>
        {
            cors.AddPolicy(CorsPolicy, x =>
            {
                x.WithOrigins("*")
                    .WithMethods("POST", "PUT", "DELETE")
                    .WithHeaders("Content-Type", "Authorization");

            });
        });
        
        // Swagger documentation
        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ConfSystem",
                Version = "v1"
            });
            
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        
        // Module List generator
        services.AddModuleInfo(modules);
        
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
        services.AddAuth(modules);
        
        services.AddHostedService<DatabaseInitializer>();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }
                
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
        
        // RabbitMQ
        services
            .AddConvey()
            .AddRabbitMq()
            .Build();
        
        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicy);
        app.UseErrorHandling();
        app.UseSwagger();
        app.UseSwaggerUI(swagger =>
        {
            swagger.RoutePrefix = "swagger";
            swagger.DocumentTitle = "ConfSystem Documentation";
        });
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        return app;
    }

    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
    
    public static string GetModuleName(this object value)
        => value?.GetType().GetModuleName() ?? string.Empty;
    
    public static string GetModuleName(this Type type)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace.StartsWith("ConfSystem.Modules.")
            ? type.Namespace.Split(".")[2].ToLowerInvariant()
            : string.Empty;
    }
}