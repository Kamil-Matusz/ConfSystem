using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Saga.Messages;

public record SpeakerCreated(Guid Id, string FullName) : IEvent;