using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Commands;

public record DeleteAgendaTrack(Guid Id) : ICommand;