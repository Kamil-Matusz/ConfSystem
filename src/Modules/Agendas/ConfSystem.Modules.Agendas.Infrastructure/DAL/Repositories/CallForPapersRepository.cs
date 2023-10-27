using ConfSystem.Modules.Agendas.Domain.CallForPapers.Entities;
using ConfSystem.Modules.Agendas.Domain.CallForPapers.Repositories;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Repositories;

internal sealed class CallForPapersRepository : ICallForPapersRepository
{
    private readonly AgendasDbContext _context;
    private readonly DbSet<CallForPapers> _callForPapers;

    public CallForPapersRepository(AgendasDbContext context)
    {
        _context = context;
        _callForPapers = context.CallForPapers;
    }
        
    public async Task<CallForPapers> GetAsync(ConferenceId conferenceId)
        => await _callForPapers.SingleOrDefaultAsync(cfp => cfp.ConferenceId == conferenceId);

    public async Task<bool> ExistsAsync(ConferenceId conferenceId)
        => await _callForPapers.AnyAsync(cfp => cfp.ConferenceId == conferenceId);

    public async Task AddAsync(CallForPapers callForPapers)
    {
        await _callForPapers.AddAsync(callForPapers);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CallForPapers callForPapers)
    {
        _callForPapers.Update(callForPapers);
        await _context.SaveChangesAsync();
    }
}