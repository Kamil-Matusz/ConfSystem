using ConfSystem.Modules.Tickets.Core.Entities;

namespace ConfSystem.Modules.Tickets.Core.DAL.Repositories.Conferences;

internal interface IConferenceRepository
{
    Task<Conference> GetConferenceAsync(Guid id);
    Task AddConferenceAsync(Conference conference);
    Task UpdateConferenceAsync(Conference conference);
    Task DeleteConferenceAsync(Conference conference);
}