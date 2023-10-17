using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Agendas.Application.Submissions.Commands;

public record RejectSubmission(Guid Id) : ICommand;