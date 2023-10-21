using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Attendances.Domain.Exceptions;

public class ParticipantNotFoundException : CustomException
{
    public Guid ConferenceId { get; }
    public Guid ParticipantId { get; }

    public ParticipantNotFoundException(Guid conferenceId, Guid participantId) 
        : base($"Participant of conference: '{conferenceId}' with participant ID: '{participantId}' was not found.")
    {
        ConferenceId = conferenceId;
        ParticipantId = participantId;
    }
}