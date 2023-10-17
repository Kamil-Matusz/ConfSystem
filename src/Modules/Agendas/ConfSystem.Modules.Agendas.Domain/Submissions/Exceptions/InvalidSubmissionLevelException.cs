using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Submissions.Exceptions;

public class InvalidSubmissionLevelException : CustomException
{
    public Guid SubmissionId { get; }

    public InvalidSubmissionLevelException(Guid submissionId) 
        : base($"Submission with ID: '{submissionId}' defines invalid level.")
        => SubmissionId = submissionId;
}