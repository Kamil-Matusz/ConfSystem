using ConfSystem.Services.Tickets.Core.Entities;

namespace ConfSystem.Services.Tickets.Core.DAL.Repositories.Conferences;

internal interface IConferenceRepository
{
    Task<Conference> GetConferenceAsync(Guid id);
    Task AddConferenceAsync(Conference conference);
    Task UpdateConferenceAsync(Conference conference);
    Task DeleteConferenceAsync(Conference conference);
}