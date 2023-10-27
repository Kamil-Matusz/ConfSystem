using ConfSystem.Modules.Agendas.Application.Agendas.DTO;
using ConfSystem.Shared.Abstractions.Queries;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Queries;

public class GetRegularAgendaSlot : IQuery<RegularAgendaSlotDto>
{
    public Guid AgendaItemId { get; set; }
}