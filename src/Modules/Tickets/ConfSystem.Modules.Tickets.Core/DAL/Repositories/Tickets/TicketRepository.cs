using ConfSystem.Modules.Tickets.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Tickets.Core.DAL.Repositories.Tickets;

internal class TicketRepository : ITicketRepository
{
    private readonly TicketsDbContext _ticketsDbContext;
    private readonly DbSet<Ticket> _tickets;

    public TicketRepository(TicketsDbContext ticketsDbContext)
    {
        _ticketsDbContext = ticketsDbContext;
        _tickets = _ticketsDbContext.Tickets;
    }

    public Task<Ticket> GetTicketAsync(Guid conferenceId, Guid userId) 
        => _tickets.SingleOrDefaultAsync(x => x.ConferenceId == conferenceId && x.UserId == userId);

    public Task<int> CountTicketsForConferenceAsync(Guid conferenceId) 
        => _tickets.CountAsync(x => x.ConferenceId == conferenceId);


    public async Task<IReadOnlyList<Ticket>> GetTicketsForUserAsync(Guid userId) 
        => await _tickets.Include(x => x.Conference).Where(x => x.UserId == userId).ToListAsync();

    public Task<Ticket> GetAsync(string code)
        => _tickets.SingleOrDefaultAsync(x => x.Code == code);
    
    public async Task AddTicketAsync(Ticket ticket)
    {
        await _tickets.AddAsync(ticket);
        await _ticketsDbContext.SaveChangesAsync();
    }

    public async Task AddManyTicketsAsync(IEnumerable<Ticket> ticket)
    {
        await _tickets.AddRangeAsync(ticket);
        await _ticketsDbContext.SaveChangesAsync();
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        _tickets.Update(ticket);
        await _ticketsDbContext.SaveChangesAsync();
    }

    public async Task DeleteTicketAsync(Ticket ticket)
    {
        _tickets.Remove(ticket);
        await _ticketsDbContext.SaveChangesAsync();
    }
}