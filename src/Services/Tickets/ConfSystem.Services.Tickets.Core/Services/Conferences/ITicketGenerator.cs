using ConfSystem.Services.Tickets.Core.Entities;

namespace ConfSystem.Services.Tickets.Core.Services.Conferences;

public interface ITicketGenerator
{
    Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price);
}