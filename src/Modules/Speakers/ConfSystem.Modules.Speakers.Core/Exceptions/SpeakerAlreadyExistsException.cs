using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Speakers.Core.Exceptions;

public sealed class SpeakerAlreadyExistsException : CustomException
{
    public Guid Id { get; }

    public SpeakerAlreadyExistsException(Guid id) : base($"Speaker with id: '{id}' already exists.")
        => Id = id;
}