using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.Submissions.Events.External;

public record SpeakerCreated(Guid Id, string FullName) : IEvent;