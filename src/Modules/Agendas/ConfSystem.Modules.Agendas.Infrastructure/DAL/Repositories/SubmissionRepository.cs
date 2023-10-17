using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using ConfSystem.Modules.Agendas.Domain.Submissions.Repositories;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Repositories;

internal sealed class SubmissionRepository : ISubmissionRepository
{
    private readonly AgendasDbContext _agendasDbContext;
    private readonly DbSet<Submission> _submissions;

    public SubmissionRepository(AgendasDbContext agendasDbContext)
    {
        _agendasDbContext = agendasDbContext;
        _submissions = _agendasDbContext.Submissions;
    }

    public Task<Submission> GetAsync(AggregateId id)
        => _submissions.Include(x => x.Speakers).SingleOrDefaultAsync(x => x.Id.Equals(id));

    public async Task AddAsync(Submission submission)
    {
        await _submissions.AddAsync(submission);
        await _agendasDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Submission submission)
    {
        _submissions.Update(submission);
        await _agendasDbContext.SaveChangesAsync();
    }
}