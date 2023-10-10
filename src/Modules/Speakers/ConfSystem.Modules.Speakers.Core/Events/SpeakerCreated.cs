using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Speakers.Core.Events;

public record SpeakerCreated(Guid Id, string FullName) : IEvent;