using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Infrastructure.PostgreSQL.Decorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Shared.Infrastructure.PostgreSQL;

public static class Extensions
{
    internal static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        var options = services.GetOptions<PostgresOptions>("postgres");
        services.AddSingleton(options);
        services.AddSingleton(new UnitOfWorkTypeRegistry());
            
        return services;
    }
    
    public static IServiceCollection AddTransactionalDecorators(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(TransactionalCommandHandlerDecorator<>));
        return services;
    }

    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<PostgresOptions>("postgres");
        services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        return services;
    }

    public static IServiceCollection AddUnitOfWork<TUnitOfWork, TImplementation>(this IServiceCollection services)
        where TUnitOfWork : class, IUnitOfWork where TImplementation : class, TUnitOfWork
    {
        services.AddScoped<TUnitOfWork, TImplementation>();
        services.AddScoped<IUnitOfWork, TImplementation>();

        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<TUnitOfWork>();

        return services;
    }
}