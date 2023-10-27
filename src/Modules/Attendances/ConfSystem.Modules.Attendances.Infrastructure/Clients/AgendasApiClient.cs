using ConfSystem.Modules.Agendas.Application.Agendas.Queries;
using ConfSystem.Modules.Attendances.Application.Clients.Agendas;
using ConfSystem.Modules.Attendances.Application.Clients.Agendas.DTO;
using ConfSystem.Shared.Abstractions.Modules;

namespace ConfSystem.Modules.Attendances.Infrastructure.Clients;

internal sealed class AgendasApiClient : IAgendasApiClient
{
    private readonly IModuleClient _client;

    public AgendasApiClient(IModuleClient client)
    {
        _client = client;
    }

    public Task<RegularAgendaSlotDto> GetRegularAgendaSlotAsync(Guid id)
        => _client.SendAsync<RegularAgendaSlotDto>("agendas/slots/regular/get",
            new GetRegularAgendaSlot
            {
                AgendaItemId = id
            });

    public Task<IEnumerable<AgendaTrackDto>> GetAgendaAsync(Guid conferenceId)
        => _client.SendAsync<IEnumerable<AgendaTrackDto>>("agendas/agendas/get", new GetAgenda
        {
            ConferenceId = conferenceId
        });
}