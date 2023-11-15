using ConfSystem.Services.Tickets.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Services.Tickets.Core.DAL.Repositories.Conferences;

internal class ConferenceRepository : IConferenceRepository
{
    
    private readonly TicketsDbContext _ticketsDbContext;
    private readonly DbSet<Conference> _conferences;

    public ConferenceRepository(TicketsDbContext ticketsDbContext)
    {
        _ticketsDbContext = ticketsDbContext;
        _conferences = _ticketsDbContext.Conferences;
    }
    
    public Task<Conference> GetConferenceAsync(Guid id) 
        =>_conferences.SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddConferenceAsync(Conference conference)
    {
        await _conferences.AddAsync(conference);
        await _ticketsDbContext.SaveChangesAsync();
    }

    public async Task UpdateConferenceAsync(Conference conference)
    {
        _conferences.Update(conference);
        await _ticketsDbContext.SaveChangesAsync();
    }

    public async Task DeleteConferenceAsync(Conference conference)
    {
        _conferences.Remove(conference);
        await _ticketsDbContext.SaveChangesAsync();
    }
}