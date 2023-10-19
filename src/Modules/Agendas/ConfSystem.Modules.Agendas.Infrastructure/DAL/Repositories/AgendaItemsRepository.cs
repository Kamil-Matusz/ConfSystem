using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Modules.Agendas.Domain.Agendas.Repositories;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Repositories;

internal sealed class AgendaItemsRepository : IAgendaItemsRepository
{
    private readonly AgendasDbContext _agendasDbContext;
    private readonly DbSet<AgendaItem> _agendaItems;

    public AgendaItemsRepository(AgendasDbContext agendasDbContext)
    {
        _agendasDbContext = agendasDbContext;
        _agendaItems = agendasDbContext.AgendaItems;
    }

    public async Task<IEnumerable<AgendaItem>> BrowseAsync(IEnumerable<SpeakerId> speakerIds)
    {
        var ids = speakerIds.Select(id => (Guid) id).ToList();
        return await _agendaItems.AsNoTracking().Include(ai => ai.Speakers)
            .Where(ai => ai.Speakers.Any(s => ids.Contains(s.Id))).ToListAsync();
    }
        
    public async Task<AgendaItem> GetAsync(AggregateId id)
        => await _agendaItems
            .Include(ai => ai.Speakers)
            .Include(ai => ai.AgendaSlot)
            .SingleOrDefaultAsync(ai => ai.Id == id);
        
    public async Task AddAsync(AgendaItem agendaItem)
    {
        _agendasDbContext.AttachRange(agendaItem.Speakers);
        await _agendaItems.AddAsync(agendaItem);
        await _agendasDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AgendaItem agendaItem)
    {
        _agendaItems.Update(agendaItem);
        await _agendasDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(AgendaItem agendaItem)
    {
        _agendaItems.Remove(agendaItem);
        await _agendasDbContext.SaveChangesAsync();
    }
}