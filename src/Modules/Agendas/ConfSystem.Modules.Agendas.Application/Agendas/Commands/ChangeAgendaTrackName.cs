using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Commands;

public record ChangeAgendaTrackName(Guid Id, string Name) : ICommand;