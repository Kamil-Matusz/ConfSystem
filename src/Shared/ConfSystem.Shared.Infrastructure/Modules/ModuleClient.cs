using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Infrastructure.Modules.ModuleSerializer;

namespace ConfSystem.Shared.Infrastructure.Modules;

internal sealed class ModuleClient : IModuleClient
{
    private readonly IModuleRegistry _moduleRegistry;
    private readonly IModuleSerializer _moduleSerializer;

    public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
    {
        _moduleRegistry = moduleRegistry;
        _moduleSerializer = moduleSerializer;
    }

    public async Task PublishAsync(object message)
    {
        var key = message.GetType().Name;
        var registrations = _moduleRegistry.GetModuleBroadcastRegistrations(key);

        var tasks = new List<Task>();
        
        foreach (var registration in registrations)
        {
            var action = registration.Action;
            var receiverMessage = TranslateType(message, registration.ReceiverType);
            tasks.Add(registration.Action(message));
        }

        await Task.WhenAll(tasks);
    }

    private object TranslateType(object value, Type type)
        => _moduleSerializer.Deserialize(_moduleSerializer.Serializer(value), type);
}