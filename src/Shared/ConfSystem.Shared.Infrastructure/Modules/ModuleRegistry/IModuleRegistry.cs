using System.Collections;

namespace ConfSystem.Shared.Infrastructure.Modules;

public interface IModuleRegistry
{
    void AddBroadcastAction(Type requestType, Func<object, Task> action);
    IEnumerable<ModuleBroadcastRegistration> GetModuleBroadcastRegistrations(string key);
}