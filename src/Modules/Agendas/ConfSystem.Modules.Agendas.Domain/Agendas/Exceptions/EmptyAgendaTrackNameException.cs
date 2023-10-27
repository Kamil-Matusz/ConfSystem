using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class EmptyAgendaTrackNameException : CustomException
{
    public EmptyAgendaTrackNameException(Guid agendaTrackId) 
        : base($"Agenda track with ID: {agendaTrackId} defines empty name.")
    {
    }
}