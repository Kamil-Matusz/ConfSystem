using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class CollidingSpeakerAgendaSlotsException : CustomException
{
    public CollidingSpeakerAgendaSlotsException(Guid agendaSlotId, Guid agendaItemId) 
        : base($"Cannot assign agenda item with ID: '{agendaItemId}' to slot with ID: '{agendaSlotId}'")
    {
    }
}