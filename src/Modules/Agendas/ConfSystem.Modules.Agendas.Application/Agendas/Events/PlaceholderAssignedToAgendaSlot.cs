using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Events;

public record PlaceholderAssignedToAgendaSlot(Guid Id, string Placeholder) : IEvent;