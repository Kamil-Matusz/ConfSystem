using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Commands;

public sealed record AssignRegularAgendaSlot(Guid AgendaTrackId, Guid AgendaSlotId, Guid AgendaItemId) : ICommand;