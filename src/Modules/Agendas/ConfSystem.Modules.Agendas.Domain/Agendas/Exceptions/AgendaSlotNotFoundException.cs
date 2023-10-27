using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class AgendaSlotNotFoundException : CustomException
{
    public AgendaSlotNotFoundException(Guid agendaSlotId) 
        : base($"Agenda slot with ID: '{agendaSlotId}' was not found.")
    {
    }
}