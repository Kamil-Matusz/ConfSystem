using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Modules.Agendas.Domain.Agendas.Repositories;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Repositories;

internal sealed class AgendaTracksRepository : IAgendaTracksRepository
{
    private readonly AgendasDbContext _agendasDbContext;
    private readonly DbSet<AgendaTrack> _agendaTracks;

    public AgendaTracksRepository(AgendasDbContext agendasDbContext)
    {
        _agendasDbContext = agendasDbContext;
        _agendaTracks = agendasDbContext.AgendaTracks;
    }

    public async Task<AgendaTrack> GetAsync(AggregateId id)
        => await _agendaTracks.Include(at => at.Slots).SingleOrDefaultAsync(at => at.Id == id);

    public async Task<IEnumerable<AgendaTrack>> BrowseAsync(ConferenceId conferenceId)
        => await _agendaTracks.AsNoTracking().Include(at => at.Slots)
            .Where(at => at.ConferenceId == conferenceId).ToListAsync();

    public async Task<bool> ExistsAsync(AggregateId id)
        => await _agendaTracks.AnyAsync(at => at.Id == id);

    public async Task AddAsync(AgendaTrack agendaTrack)
    {
        await _agendaTracks.AddAsync(agendaTrack);
        await _agendasDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AgendaTrack agendaTrack)
        => await _agendasDbContext.SaveChangesAsync();

    public async Task DeleteAsync(AgendaTrack agendaTrack)
    {
        _agendaTracks.Remove(agendaTrack);
        await _agendasDbContext.SaveChangesAsync();
    }
}