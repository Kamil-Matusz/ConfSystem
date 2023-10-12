using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Application.Exceptions;

public class SubmissionNotFoundException : CustomException
{
    public Guid SubmissionId { get;}
    public SubmissionNotFoundException(Guid submissionId) : base($"Submission with ID: '{submissionId}' was not found")
    {
        SubmissionId = submissionId;
    }
}