namespace ConfSystem.Shared.Abstractions.Modules;

public interface IModuleClient
{
    Task PublishAsync(object message);
}