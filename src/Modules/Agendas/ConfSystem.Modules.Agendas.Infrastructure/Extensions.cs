using ConfSystem.Modules.Agendas.Domain.CallForPapers.Repositories;
using ConfSystem.Modules.Agendas.Domain.Submissions.Repositories;
using ConfSystem.Modules.Agendas.Infrastructure.DAL;
using ConfSystem.Modules.Agendas.Infrastructure.DAL.Repositories;
using ConfSystem.Shared.Infrastructure.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Agendas.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<AgendasDbContext>();
        services.AddScoped<ISpeakerRepository, SpeakerRepository>();
        services.AddScoped<ISubmissionRepository, SubmissionRepository>();
        services.AddScoped<ICallForPapersRepository, CallForPapersRepository>();
        return services;
    } 
}