using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Agendas.Domain.Submissions.Repositories;

public interface ISubmissionRepository
{
    Task<Submission> GetAsync(AggregateId id);
    Task AddAsync(Submission submission);
    Task UpdateAsync(Submission submission);
}