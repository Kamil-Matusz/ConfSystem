using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Attendances.Domain.Exceptions;

public class NoFreeSlotsException : CustomException
{
    public NoFreeSlotsException() : base("No free slots left.")
    {
    }
}