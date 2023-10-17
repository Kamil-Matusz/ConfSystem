using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Domain.Submissions.Exceptions;

public class EmptySubmissionTitleException : CustomException
{
    public Guid SubmissionId { get; }

    public EmptySubmissionTitleException(Guid submissionId) 
        : base($"Submission with ID: '{submissionId}' defines empty title.")
        => SubmissionId = submissionId;
}