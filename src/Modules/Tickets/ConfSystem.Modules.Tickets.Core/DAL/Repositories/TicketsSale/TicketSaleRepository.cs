using ConfSystem.Modules.Tickets.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Tickets.Core.DAL.Repositories.TicketsSale;

internal class TicketSaleRepository : ITicketSaleRepository
{
    private readonly TicketsDbContext _ticketsDbContext;
    private readonly DbSet<TicketSale> _ticketSales;

    public TicketSaleRepository(TicketsDbContext ticketsDbContext)
    {
        _ticketsDbContext = ticketsDbContext;
        _ticketSales = _ticketsDbContext.TicketSales;
    }


    public Task<TicketSale> GetAsync(Guid id)
        => _ticketSales
            .Include(x => x.Tickets)
            .FirstOrDefaultAsync(x => x.Id == id);

    public Task<TicketSale> GetCurrentForConferenceAsync(Guid conferenceId, DateTime now)
        => _ticketSales
            .Where(x => x.ConferenceId == conferenceId)
            .OrderBy(x => x.From)
            .Include(x => x.Tickets)
            .LastOrDefaultAsync(x => x.From <= now && x.To >= now);

    public async Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId)
        => await _ticketSales
            .AsNoTracking()
            .Where(x => x.ConferenceId == conferenceId)
            .Include(x => x.Tickets)
            .ToListAsync();

    public async Task AddAsync(TicketSale ticketSale)
    {
        await _ticketSales.AddAsync(ticketSale);
        await _ticketsDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TicketSale ticketSale)
    {
        _ticketSales.Update(ticketSale);
        await _ticketsDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TicketSale ticketSale)
    {
        _ticketSales.Remove(ticketSale);
        await _ticketsDbContext.SaveChangesAsync();
    }
}