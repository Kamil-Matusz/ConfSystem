using ConfSystem.Modules.Agendas.Application.Agendas.DTO;
using ConfSystem.Shared.Abstractions.Queries;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Queries;

public class BrowseAgendaItems : IQuery<IEnumerable<AgendaItemDto>>
{
    public Guid ConferenceId { get; set; }
}