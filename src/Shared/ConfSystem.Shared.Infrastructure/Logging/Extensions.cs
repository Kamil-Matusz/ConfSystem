using ConfSystem.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ConfSystem.Shared.Infrastructure.Logging;

public static class Extensions
{
    internal static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

        return services;
    }

    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog(((context, configuration) =>
        {
            configuration
                .WriteTo.File("logs/logs.txt");
        } ));

        return builder;
    }
}