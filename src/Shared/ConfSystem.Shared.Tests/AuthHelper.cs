using ConfSystem.Shared.Infrastructure.Auth;
using ConfSystem.Shared.Infrastructure.Services;

namespace ConfSystem.Shared.Tests;

public static class AuthHelper
{
    private static readonly AuthManager AuthManager;

    static AuthHelper()
    {
        var options = OptionsHelper.GetOptions<AuthOptions>("auth");
        AuthManager = new AuthManager(options, new Clock());
    }
    
    public static string GenerateJwt(string userId, string role = null, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null)
        => AuthManager.CreateToken(userId, role, audience, claims).AccessToken;
}