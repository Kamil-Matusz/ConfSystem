using System.Runtime.CompilerServices;
using ConfSystem.Modules.Users.Core.DAL;
using ConfSystem.Modules.Users.Core.DAL.Repositories;
using ConfSystem.Modules.Users.Core.Entities;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Modules.Users.Api")]
namespace ConfSystem.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<UsersDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        return services;
    }
}