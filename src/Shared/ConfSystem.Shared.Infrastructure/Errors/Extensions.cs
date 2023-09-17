using ConfSystem.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Shared.Infrastructure.Errors;

internal static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        => services
            .AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<IExceptionMapper, ExceptionMapper>()
            .AddSingleton<IExceptionCompositionRoot,ExceptionCompositionRoot>();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandlerMiddleware>();
}