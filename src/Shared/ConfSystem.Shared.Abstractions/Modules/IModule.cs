using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Shared.Abstractions.Modules;

public interface IModule
{
    string Name { get; }
    string Path { get; }
    IEnumerable<string> Policies => null;
}