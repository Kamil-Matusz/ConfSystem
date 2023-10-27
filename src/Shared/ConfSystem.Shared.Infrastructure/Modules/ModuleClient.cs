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

    public async Task<TResult> SendAsync<TResult>(string path, object request) where TResult : class
    {
        var registration = _moduleRegistry.GetRequestRegistration(path);
        if (registration is null)
        {
            throw new InvalidOperationException($"No action has benn defined for path: '{path}'");
        }

        var receiverRequest = TranslateType(request, registration.ReceiverType);
        var result = await registration.Action(receiverRequest);

        return result is null ? null : TranslateType<TResult>(result);
    }

    private object TranslateType(object value, Type type)
        => _moduleSerializer.Deserialize(_moduleSerializer.Serializer(value), type);

    private T TranslateType<T>(object value)
        => _moduleSerializer.Deserialize<T>(_moduleSerializer.Serializer(value));
}