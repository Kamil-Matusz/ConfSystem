using ConfSystem.Shared.Abstractions.Modules;

namespace ConfSystem.Modules.Speakers.Api;

internal class SpeakersModule : IModule
{
    public const string BasePath = "speakers-module";        
    public string Name { get; } = "Speakers";
    public string Path => BasePath;

    public IEnumerable<string> Policies { get; } = new[] {"speakers"};
    
}