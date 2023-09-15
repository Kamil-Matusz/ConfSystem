using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Conferences.Core.Exceptions;

internal class CannotDeleteHostException : CustomException
{
    public Guid Id { get; }
    
    public CannotDeleteHostException(Guid id) : base($"Host with ID: '{id}' cannot be deleted.")
    {
        Id = id;
    }
}