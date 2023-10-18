using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Agendas.Domain.CallForPapers.Repositories;

public interface ICallForPapersRepository
{
    public Task<Entities.CallForPapers> GetAsync(ConferenceId conferenceId);
    public Task<bool> ExistsAsync(ConferenceId conferenceId);
    public Task AddAsync(Entities.CallForPapers callForPapers);
    public Task UpdateAsync(Entities.CallForPapers callForPapers);
}