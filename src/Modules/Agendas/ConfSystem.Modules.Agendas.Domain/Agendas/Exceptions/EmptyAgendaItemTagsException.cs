using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class EmptyAgendaItemTagsException : CustomException
{
    public Guid AgendaItemId { get; }
        
    public EmptyAgendaItemTagsException(Guid agendaItemId) 
        : base($"Agenda Item with id: '{agendaItemId}' defines empty tags.")
        => AgendaItemId = agendaItemId;
}