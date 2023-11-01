using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("ConfSystem.Modules.Attandences.Tests.Unit")]
[assembly:InternalsVisibleTo("DynamicProxyAssemblyGen2")]
namespace ConfSystem.Modules.Attendances.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    } 
}