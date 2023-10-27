namespace ConfSystem.Shared.Infrastructure.Modules;

public interface IModuleRegistry
{
    void AddBroadcastAction(Type requestType, Func<object, Task> action);
    IEnumerable<ModuleBroadcastRegistration> GetModuleBroadcastRegistrations(string key);
    void AddRequestAction(string path, Type requestType, Type responseType, Func<object, Task<object>> action);
    ModuleRequestRegistration GetRequestRegistration(string path);
}