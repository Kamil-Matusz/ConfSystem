using ConfSystem.Modules.Conferences.Core.Entities;
using ConfSystem.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Conferences.Core.DAL.Repositories;

internal class HostRepository : IHostRepository
{
    private readonly ConferencesDbContext _conferencesDbContext;
    private readonly DbSet<Host> _hosts;

    public HostRepository(ConferencesDbContext conferencesDbContext)
    {
        _conferencesDbContext = conferencesDbContext;
        _hosts = _conferencesDbContext.Hosts;
    }

    public async Task<Host> GetAsync(Guid id) => await _hosts
        .Include(x => x.Conferences)
        .SingleOrDefaultAsync(x => x.HostId == id);

    public async Task<IReadOnlyList<Host>> GetAllAsync() => await _hosts.ToListAsync();

    public async Task AddAsync(Host host)
    {
        await _hosts.AddAsync(host);
        await _conferencesDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Host host)
    {
        _hosts.Update(host);
        await _conferencesDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Host host)
    {
        _hosts.Remove(host);
        await _conferencesDbContext.SaveChangesAsync();
    }
}