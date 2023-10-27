using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class NegativeParticipantsLimitException : CustomException
{
    public NegativeParticipantsLimitException(Guid agendaSlotId) 
        : base($"Regular slot with ID: '{agendaSlotId}' defines negative participants limit.")
    {
    }
}