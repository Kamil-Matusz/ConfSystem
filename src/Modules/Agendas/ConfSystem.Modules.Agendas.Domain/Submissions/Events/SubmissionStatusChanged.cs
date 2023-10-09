using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using ConfSystem.Shared.Abstractions.Kernel;

namespace ConfSystem.Modules.Agendas.Domain.Submissions.Events;

public record SubmissionStatusChanged(Submission Submission, string Status) : IDomainEvent;