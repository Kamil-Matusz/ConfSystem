using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Commands;

public record CreateAgendaTrack(Guid ConferenceId, string Name) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}