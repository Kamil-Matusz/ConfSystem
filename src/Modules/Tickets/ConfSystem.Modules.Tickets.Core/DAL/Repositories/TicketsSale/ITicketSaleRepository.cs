using ConfSystem.Modules.Tickets.Core.Entities;

namespace ConfSystem.Modules.Tickets.Core.DAL.Repositories.TicketsSale;

internal interface ITicketSaleRepository
{
    Task<TicketSale> GetAsync(Guid id);
    Task<TicketSale> GetCurrentForConferenceAsync(Guid conferenceId, DateTime now);
    Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId);
    Task AddAsync(TicketSale ticketSale);
    Task UpdateAsync(TicketSale ticketSale);
    Task DeleteAsync(TicketSale ticketSale);
}