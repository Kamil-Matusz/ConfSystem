using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Attendances.Application.Events;

internal record TicketPurchased(Guid TicketId, Guid ConferenceId, Guid UserId) : IEvent;