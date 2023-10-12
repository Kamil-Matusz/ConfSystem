using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Agendas.Application.Submissions.Commands;

public record CreateSubmission(Guid ConferenceId, string Title, string Description, int Level, IEnumerable<string> Tags,
    IEnumerable<Guid> SpeakerIds) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}