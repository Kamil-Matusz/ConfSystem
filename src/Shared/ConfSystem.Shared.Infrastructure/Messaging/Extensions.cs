using ConfSystem.Shared.Abstractions.Messaging;
using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Infrastructure.Messaging.Brokers;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Shared.Infrastructure.Messaging;

internal static class Extensions
{
    internal static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
        return services;
    }
}