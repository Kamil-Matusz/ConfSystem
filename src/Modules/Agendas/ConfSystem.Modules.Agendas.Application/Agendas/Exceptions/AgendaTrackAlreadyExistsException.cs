using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Exceptions;

public class AgendaTrackAlreadyExistsException : CustomException
{
    public AgendaTrackAlreadyExistsException(Guid agendaTrackId) 
        : base($"Agenda track with ID: '{agendaTrackId} already exists.'")
    {
    }
}