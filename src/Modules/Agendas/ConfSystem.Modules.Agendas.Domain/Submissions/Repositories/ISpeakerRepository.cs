using ConfSystem.Modules.Agendas.Domain.Submissions.Entities;
using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Agendas.Domain.Submissions.Repositories;

public interface ISpeakerRepository
{
    Task<bool> ExistsAsync(AggregateId id);
    Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids);
    Task AddAsync(Speaker speaker);
}