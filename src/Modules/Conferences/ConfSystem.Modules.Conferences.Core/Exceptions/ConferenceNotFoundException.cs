using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Conferences.Core.Exceptions;

internal class ConferenceNotFoundException : CustomException
{
    public Guid Id { get;}
    public ConferenceNotFoundException(Guid id) : base($"Conference with ID: '{id}' was not found.")
    {
        Id = id;
    }
}