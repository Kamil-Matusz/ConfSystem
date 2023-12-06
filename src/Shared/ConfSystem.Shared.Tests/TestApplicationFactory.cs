using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

[assembly: InternalsVisibleTo("ConfSystem.Modules.Attandences.Tests.Integration")]
[assembly: InternalsVisibleTo("ConfSystem.Modules.Conferences.Tests.Integration")]
[assembly: InternalsVisibleTo("ConfSystem.Modules.Users.Tests.Integration")]
namespace ConfSystem.Shared.Tests;

public sealed class TestApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("test");
    }
}