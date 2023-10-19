using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Exceptions;

public class AgendaSlotTypeNotFoundException : CustomException
{
    public string Type { get; }

    public AgendaSlotTypeNotFoundException(string type) : base($"Agenda slot type: '{type}' was not found.")
    {
        Type = type;
    }
}