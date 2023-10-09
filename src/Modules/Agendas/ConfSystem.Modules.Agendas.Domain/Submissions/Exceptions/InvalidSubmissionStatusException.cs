using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Submissions.Exceptions;

public class InvalidSubmissionStatusException : CustomException
{
    public Guid SubmissionId { get; }

    public InvalidSubmissionStatusException(Guid submissionId, string desiredStatus, string currentStatus) 
        : base($"Cannot change status of submission with ID: '{submissionId}' from {currentStatus} to {desiredStatus}.")
        => SubmissionId = submissionId;
}