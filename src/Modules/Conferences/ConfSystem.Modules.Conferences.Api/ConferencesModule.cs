using ConfSystem.Modules.Conferences.Core;
using ConfSystem.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Conferences.Api;

internal class ConferencesModule : IModule
{
    public const string BasePath = "conferences-module";
    public string Name { get; } = "Conferences";
    public string Path => BasePath;

    public IEnumerable<string> Policies { get; } = new[] {"conferences", "hosts"};
}