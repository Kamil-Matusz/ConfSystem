using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Events;

public record AgendaItemAssignedToAgendaSlot(Guid Id, Guid AgendaItemId) : IEvent;