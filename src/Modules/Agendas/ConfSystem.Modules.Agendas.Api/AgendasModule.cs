using ConfSystem.Modules.Agendas.Application.Agendas.DTO;
using ConfSystem.Modules.Agendas.Application.Agendas.Queries;
using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Shared.Abstractions.Modules;
using ConfSystem.Shared.Abstractions.Queries;
using ConfSystem.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ConfSystem.Modules.Agendas.Api;

internal class AgendasModule : IModule
{
    public const string BasePath = "agendas-module";
    public string Name { get; } = "Agendas";
    public string Path => BasePath;

    //public IEnumerable<string> Policies { get; } = new[] {"conferences", "hosts"};

    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests()
            .Subscribe<GetRegularAgendaSlot, RegularAgendaSlotDto>("agendas/slots/regular/get",
                (query, sp) => sp.GetRequiredService<IQueryDispatcher>().QueryAsync(query));
    }
}