using ConfSystem.Modules.Tickets.Core.DTO;

namespace ConfSystem.Modules.Tickets.Core.Services.Tickets;

public interface ITicketService
{
    Task PurchaseAsync(Guid conferenceId, Guid userId);
    Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId);
}