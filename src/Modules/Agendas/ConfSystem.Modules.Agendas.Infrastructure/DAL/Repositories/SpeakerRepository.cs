using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using ConfSystem.Modules.Agendas.Domain.Submissions.Repositories;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Repositories;

internal sealed class SpeakerRepository : ISpeakerRepository
{
    private readonly AgendasDbContext _agendasDbContext;
    private readonly DbSet<Speaker> _speakers;

    public SpeakerRepository(AgendasDbContext agendasDbContext)
    {
        _agendasDbContext = agendasDbContext;
        _speakers = _agendasDbContext.Speakers;
    }

    public Task<bool> ExistsAsync(AggregateId id)
        => _speakers.AnyAsync(x => x.Id.Equals(id));

    public async Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids)
        => await _speakers.Where(x => ids.Contains(x.Id)).ToListAsync();

    public async Task AddAsync(Speaker speaker)
    {
        await _speakers.AddAsync(speaker);
        await _agendasDbContext.SaveChangesAsync();
    }
}