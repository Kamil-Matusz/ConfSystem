using ConfSystem.Modules.Conferences.Core.Entities;
using ConfSystem.Modules.Conferences.Core.Repositories.Conference;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Conferences.Core.DAL.Repositories;

internal class ConferenceRepository : IConferenceRepository
{
    private readonly ConferencesDbContext _conferencesDbContext;
    private readonly DbSet<Conference> _conferences;

    public ConferenceRepository(ConferencesDbContext conferencesDbContext)
    {
        _conferencesDbContext = conferencesDbContext;
        _conferences = _conferencesDbContext.Conferences;
    }

    public async Task<Conference> GetAsync(Guid id) => await _conferences
        .Include(x => x.Host)
        .SingleOrDefaultAsync(x => x.ConferenceId == id);

    public async Task<IReadOnlyList<Conference>> GetAllAsync() => await _conferences
        .Include(x => x.Host)
        .ToListAsync();

    public async Task AddAsync(Conference conference)
    {
        await _conferences.AddAsync(conference);
        await _conferencesDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Conference conference)
    {
        _conferences.Update(conference);
        await _conferencesDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Conference conference)
    {
        _conferences.Remove(conference);
        await _conferencesDbContext.SaveChangesAsync();
    }
}