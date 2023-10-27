using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Attendances.Domain.Exceptions;

public class AlreadyParticipatingSameTimeException : CustomException
{
    public AlreadyParticipatingSameTimeException() : base("Already participating in the same time.")
    {
    }
}