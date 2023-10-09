using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Submissions.Exceptions;

public class MissingSubmissionSpeakersException : CustomException
{
    public Guid SubmissionId { get; }

    public MissingSubmissionSpeakersException(Guid submissionId) 
        : base($"Submission with ID: '{submissionId}' has missing speakers.")
        => SubmissionId = submissionId;
}