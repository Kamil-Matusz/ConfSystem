using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Submissions.Exceptions;

public class EmptySubmissionDescriptionException : CustomException
{
    public Guid SubmissionId { get; }

    public EmptySubmissionDescriptionException(Guid submissionId) 
        : base($"Submission with ID: '{submissionId}' defines empty description.")
        => SubmissionId = submissionId;
}