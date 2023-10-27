using ConfSystem.Modules.Attendances.Application.Clients.Agendas.DTO;

namespace ConfSystem.Modules.Attendances.Application.Clients.Agendas;

public interface IAgendasApiClient
{
    Task<RegularAgendaSlotDto> GetRegularAgendaSlotAsync(Guid id);
    Task<IEnumerable<AgendaTrackDto>> GetAgendaAsync(Guid conferenceId);
}