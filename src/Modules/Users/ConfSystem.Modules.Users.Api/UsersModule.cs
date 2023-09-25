using ConfSystem.Shared.Abstractions.Modules;

namespace ConfSystem.Modules.Users.Api;

internal class UsersModule : IModule
{
    public const string BasePath = "users-module";        
    public string Name { get; } = "Users";
    public string Path => BasePath;
        
    public IEnumerable<string> Policies { get; } = new[]
    {
        "users"
    };
    
}