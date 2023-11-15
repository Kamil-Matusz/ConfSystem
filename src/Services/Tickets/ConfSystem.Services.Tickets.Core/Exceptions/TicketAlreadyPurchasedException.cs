using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Services.Tickets.Core.Exceptions;

public sealed class TicketAlreadyPurchasedException : CustomException
{
    public Guid ConferenceId { get; }
    public Guid UserId { get; }

    public TicketAlreadyPurchasedException(Guid conferenceId, Guid userId)
        : base("Ticket for the conference has been already purchased.")

    {
        ConferenceId = conferenceId;
        UserId = userId;
    }
}