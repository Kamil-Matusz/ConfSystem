using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Services.Tickets.Core.Exceptions;

internal sealed class TicketsUnavailableException : CustomException
{
    public Guid ConferenceId { get; }

    public TicketsUnavailableException(Guid conferenceId)
        : base("There are no available tickets for the conference.")

    {
        ConferenceId = conferenceId;
    }
}