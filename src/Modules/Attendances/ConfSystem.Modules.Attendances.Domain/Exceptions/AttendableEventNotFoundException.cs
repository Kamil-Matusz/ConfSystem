using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Attendances.Domain.Exceptions;

public class AttendableEventNotFoundException : CustomException
{
    public Guid Id { get; }

    public AttendableEventNotFoundException(Guid id) 
        : base($"Attendable event with ID: '{id}' was not found.")
    {
        Id = id;
    }
}