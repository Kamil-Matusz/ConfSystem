using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Tickets.Core.Exceptions;

internal sealed class TooManyTicketsException : CustomException
{
    public Guid ConferenceId { get; }

    public TooManyTicketsException(Guid conferenceId)
        : base("Too many tickets would be generated for the conference.")

    {
        ConferenceId = conferenceId;
    }
}