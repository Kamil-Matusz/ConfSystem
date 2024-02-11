using System.Runtime.CompilerServices;
using ConfSystem.Modules.Users.Core.DAL;
using ConfSystem.Modules.Users.Core.DAL.Repositories;
using ConfSystem.Modules.Users.Core.DTO;
using ConfSystem.Modules.Users.Core.Entities;
using ConfSystem.Modules.Users.Core.Services;
using ConfSystem.Modules.Users.Core.Validators;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Modules.Users.Api")]
namespace ConfSystem.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddFluentValidation(fv => fv
            .RegisterValidatorsFromAssemblyContaining<SignInDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<SignUpDtoValidator>());
        
        services.AddPostgres<UsersDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}