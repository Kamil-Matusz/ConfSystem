using ConfSystem.Modules.Agendas.Application.Agendas.DTO;
using ConfSystem.Shared.Abstractions.Queries;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Queries;

public class GetAgendaTrack : IQuery<AgendaTrackDto>
{
    public Guid Id { get; set; }
}