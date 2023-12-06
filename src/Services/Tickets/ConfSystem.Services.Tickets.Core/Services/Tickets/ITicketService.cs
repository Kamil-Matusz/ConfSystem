using ConfSystem.Services.Tickets.Core.DTO;

namespace ConfSystem.Services.Tickets.Core.Services.Tickets;

public interface ITicketService
{
    Task PurchaseAsync(Guid conferenceId, Guid userId);
    Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId);
}