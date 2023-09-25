using ConfSystem.Modules.Tickets.Core.Entities;

namespace ConfSystem.Modules.Tickets.Core.Services.Conferences;

public interface ITicketGenerator
{
    Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price);
}