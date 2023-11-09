using Chronicle;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Saga;

public static class Extension
{
    public static IServiceCollection AddSaga(this IServiceCollection services)
    {
        services.AddChronicle();
        return services;
    }
}