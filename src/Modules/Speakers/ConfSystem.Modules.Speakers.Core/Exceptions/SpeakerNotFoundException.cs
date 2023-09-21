using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Speakers.Core.Exceptions;

public sealed class SpeakerNotFoundException : CustomException
{
    public Guid Id { get; }

    public SpeakerNotFoundException(Guid id) : base($"Speaker with id '{id} was not found.'")
        => Id = id;
}