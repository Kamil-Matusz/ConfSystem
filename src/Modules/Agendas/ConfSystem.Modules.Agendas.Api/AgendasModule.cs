using ConfSystem.Shared.Abstractions.Modules;

namespace ConfSystem.Modules.Agendas.Api;

internal class AgendasModule : IModule
{
    public const string BasePath = "agendas-module";
    public string Name { get; } = "Agendas";
    public string Path => BasePath;

    //public IEnumerable<string> Policies { get; } = new[] {"conferences", "hosts"};
}