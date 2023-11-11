namespace ConfSystem.Shared.Abstractions.Modules;

public interface IModuleClient
{
    Task PublishAsync(object message);
    Task<TResult> SendAsync<TResult>(string path, object request) where TResult : class;
    Task SendAsync(string path, object request);
}