using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Repositories;

public interface IAgendaItemsRepository
{
    Task<IEnumerable<AgendaItem>> BrowseAsync(IEnumerable<SpeakerId> speakerIds);
    Task<AgendaItem> GetAsync(AggregateId id);
    Task AddAsync(AgendaItem agendaItem);
    Task UpdateAsync(AgendaItem agendaItem);
    Task DeleteAsync(AgendaItem agendaItem);
}