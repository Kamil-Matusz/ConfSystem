using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Exceptions;

public class ConflictingAgendaSlotsException : CustomException
{
    public ConflictingAgendaSlotsException(DateTime from, DateTime to) 
        : base($"There is slot conflicting with date range: {from} | {to}.")
    {
    }
}