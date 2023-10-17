using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Agendas.Application.Exceptions;

public class InvalidSpeakersNumberException : CustomException
{
    public Guid SubmissionId { get;}
    public InvalidSpeakersNumberException(Guid submissionId) : base($"Submission with ID: '{submissionId}' has invalid number of spekaers")
    {
        SubmissionId = submissionId;
    }
}