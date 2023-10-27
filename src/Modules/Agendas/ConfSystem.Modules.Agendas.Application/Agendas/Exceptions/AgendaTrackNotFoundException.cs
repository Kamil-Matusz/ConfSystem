using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Exceptions;

public class AgendaTrackNotFoundException : CustomException
{
    public AgendaTrackNotFoundException(Guid agendaTrackId) 
        : base($"Agenda track with ID: '{agendaTrackId} was not found.'")
    {
    }
}