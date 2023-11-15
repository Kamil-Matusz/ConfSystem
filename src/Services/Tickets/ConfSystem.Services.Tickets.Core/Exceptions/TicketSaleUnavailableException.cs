using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Services.Tickets.Core.Exceptions;

internal sealed class TicketSaleUnavailableException : CustomException
{
    public Guid ConferenceId { get; }

    public TicketSaleUnavailableException(Guid conferenceId)
        : base("Ticket sale for the conference is unavailable.")

    {
        ConferenceId = conferenceId;
    }
}