using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class AgendaItemNotFoundException : CustomException
{
    public AgendaItemNotFoundException(Guid agendaItemId) 
        : base($"Agenda item with ID: '{agendaItemId}' was not found.")
    {
    }
}