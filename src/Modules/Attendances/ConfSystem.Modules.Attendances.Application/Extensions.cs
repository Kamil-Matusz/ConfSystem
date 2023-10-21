using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Attendances.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    } 
}