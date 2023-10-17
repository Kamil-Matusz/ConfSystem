using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.Submissions.Events;

public record SubmissionCreated(Guid Id) : IEvent;