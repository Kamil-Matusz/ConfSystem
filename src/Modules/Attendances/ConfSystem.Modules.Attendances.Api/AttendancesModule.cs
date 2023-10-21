using ConfSystem.Shared.Abstractions.Modules;

namespace ConfSystem.Modules.Attendances.Api;

internal class AttendancesModule : IModule
{
    public const string BasePath = "attendances-module";        
    public string Name { get; } = "Attendances";
    public string Path => BasePath;
}