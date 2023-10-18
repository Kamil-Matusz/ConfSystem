using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Application.CallForPapers.Exceptions;

internal class CallForPapersClosedException : CustomException
{
    public Guid ConferenceId { get; }

    public CallForPapersClosedException(Guid conferenceId)
        : base($"Conference with ID: '{conferenceId}' has closed CFP.")
        => ConferenceId = conferenceId;
}