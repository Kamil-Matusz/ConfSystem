using ConfSystem.Shared.Abstractions.Modules;

namespace ConfSystem.Modules.Saga;

public class SagaModule : IModule
{
    public const string BasePath = "saga-module";
    public string Name { get; } = "Saga";
    public string Path => BasePath;

}