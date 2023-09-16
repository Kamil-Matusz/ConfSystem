using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Conferences.Core.Exceptions;

internal class CannotDeleteConferenceException : CustomException
{
    public Guid Id { get; }
    
    public CannotDeleteConferenceException(Guid id) : base($"Conference with ID: '{id}' cannot be deleted.")
    {
        Id = id;
    }
}