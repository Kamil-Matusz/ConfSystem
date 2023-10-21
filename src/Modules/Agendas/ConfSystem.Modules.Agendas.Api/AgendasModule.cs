using ConfSystem.Modules.Agendas.Application.Agendas.DTO;
using ConfSystem.Modules.Agendas.Application.Agendas.Queries;
using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Builder;

namespace ConfSystem.Modules.Agendas.Api;

internal class AgendasModule : IModule
{
    public const string BasePath = "agendas-module";
    public string Name { get; } = "Agendas";
    public string Path => BasePath;

    //public IEnumerable<string> Policies { get; } = new[] {"conferences", "hosts"};
}