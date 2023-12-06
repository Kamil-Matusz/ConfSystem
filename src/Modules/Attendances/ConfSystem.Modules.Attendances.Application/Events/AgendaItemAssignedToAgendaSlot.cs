using ConfSystem.Shared.Abstractions.Events;
namespace ConfSystem.Modules.Attendances.Application.Events;

internal record AgendaItemAssignedToAgendaSlot(Guid Id, Guid AgendaItemId) : IEvent;