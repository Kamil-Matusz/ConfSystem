using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class EmptyAgendaSlotPlaceholderException : CustomException
{
    public EmptyAgendaSlotPlaceholderException() 
        : base($"Agenda slot defined empty placeholder'")
    {
    }
}