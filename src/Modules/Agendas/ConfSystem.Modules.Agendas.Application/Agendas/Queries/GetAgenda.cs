using ConfSystem.Modules.Agendas.Application.Agendas.DTO;
using ConfSystem.Shared.Abstractions.Queries;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Queries;

public class GetAgenda : IQuery<IEnumerable<AgendaTrackDto>>
{
    public Guid ConferenceId { get; set; }
}