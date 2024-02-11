using System.Runtime.CompilerServices;
using ConfSystem.Modules.Speakers.Core.DAL;
using ConfSystem.Modules.Speakers.Core.DAL.Repositories;
using ConfSystem.Modules.Speakers.Core.Services;
using ConfSystem.Modules.Speakers.Core.Validators;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ConfSystem.Modules.Speakers.Api")]
namespace ConfSystem.Modules.Speakers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddFluentValidation(fv => fv
            .RegisterValidatorsFromAssemblyContaining<SpeakerDtoValidator>());
        
        services.AddPostgres<SpeakersDbContext>();
        services.AddScoped<ISpeakersRepository, SpeakersRepository>();
        services.AddScoped<ISpeakersService, SpeakersService>();
        return services;
    }
}