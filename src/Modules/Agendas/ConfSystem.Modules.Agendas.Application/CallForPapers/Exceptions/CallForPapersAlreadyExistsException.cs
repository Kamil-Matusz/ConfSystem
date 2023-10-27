using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Application.CallForPapers.Exceptions;

internal class CallForPapersAlreadyExistsException : CustomException
{
    public Guid ConferenceId { get; }

    public CallForPapersAlreadyExistsException(Guid conferenceId)
        : base($"Conference with ID: '{conferenceId}' already defined CFP.")
        => ConferenceId = conferenceId;
}