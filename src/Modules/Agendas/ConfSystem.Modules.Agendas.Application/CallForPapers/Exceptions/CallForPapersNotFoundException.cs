using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Application.CallForPapers.Exceptions;

internal class CallForPapersNotFoundException : CustomException
{
    public Guid ConferenceId { get; }

    public CallForPapersNotFoundException(Guid conferenceId)
        : base($"Conference with ID: '{conferenceId}' has no CFP.")
        => ConferenceId = conferenceId;
}