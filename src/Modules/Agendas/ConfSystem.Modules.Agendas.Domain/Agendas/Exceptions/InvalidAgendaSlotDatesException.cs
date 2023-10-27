using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class InvalidAgendaSlotDatesException : CustomException
{
    public DateTime From { get; }
    public DateTime To { get; }

    public InvalidAgendaSlotDatesException(DateTime from, DateTime to)
        : base($"Agenda track has invalid dates, from: '{from:d}' > to: '{to:d}'.")
    {
        From = from;
        To = to;
    }
}