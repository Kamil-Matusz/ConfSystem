using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Services.Tickets.Core.Events;

internal record TicketPurchased(Guid TicketId, Guid ConferenceId, Guid UserId) : IEvent;