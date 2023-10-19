using ConfSystem.Modules.Agendas.Domain.Agendas.Entities;
using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Agendas.Domain.Agendas.Repositories;

public interface IAgendaTracksRepository
{
    Task<AgendaTrack> GetAsync(AggregateId id);
    Task<IEnumerable<AgendaTrack>> BrowseAsync(ConferenceId conferenceId);
    Task<bool> ExistsAsync(AggregateId id);
    Task AddAsync(AgendaTrack agendaTrack);
    Task UpdateAsync(AgendaTrack agendaTrack);
    Task DeleteAsync(AgendaTrack agendaTrack);
}