using ConfSystem.Shared.Abstractions.Modules;

namespace ConfSystem.Modules.Tickets.Api;

internal class TicketsModule : IModule
{
    public const string BasePath = "tickets-module";        
    public string Name { get; } = "Tickets";
    public string Path => BasePath;
        
    public IEnumerable<string> Policies { get; } = new[]
    {
        "tickets"
    };
}