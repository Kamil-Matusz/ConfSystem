using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class InvalidAgendaSlotTypeException : CustomException
{
    public InvalidAgendaSlotTypeException(Guid agendaSlotId) 
        : base($"Agenda slot with ID: '{agendaSlotId}' has type which does not allow to perform desired operation.")
    {
    }
}