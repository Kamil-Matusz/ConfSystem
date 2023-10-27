using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Attendances.Domain.Exceptions;

public class AlreadyParticipatingInEventException : CustomException
{
    public AlreadyParticipatingInEventException() : base("Already participating in the selected agenda item.")
    {
    }
}