namespace ConfSystem.Modules.Conferences.Core.Repositories.Conference;

internal interface IConferenceRepository
{
    Task<Entities.Conference> GetAsync(Guid id);
    Task<IReadOnlyList<Entities.Conference>> GetAllAsync();
    Task AddAsync(Entities.Conference conference);
    Task UpdateAsync(Entities.Conference conference);
    Task DeleteAsync(Entities.Conference conference);
}