using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Commands;

public record AssignPlaceholderAgendaSlot(Guid AgendaSlotId, Guid AgendaTrackId, string Placeholder) : ICommand;