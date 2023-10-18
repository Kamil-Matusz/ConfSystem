using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.CallForPapers.Exceptions;

internal class InvalidCallForPapersDatesException : CustomException
{
    public DateTime From { get; }
    public DateTime To { get; }

    public InvalidCallForPapersDatesException(DateTime from, DateTime to)
        : base($"CFP has invalid dates, from: '{from:d}' > to: '{to:d}'.")
    {
        From = from;
        To = to;
    }
}