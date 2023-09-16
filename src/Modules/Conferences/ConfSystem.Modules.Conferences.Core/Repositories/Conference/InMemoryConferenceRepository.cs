namespace ConfSystem.Modules.Conferences.Core.Repositories.Conference;

internal class InMemoryConferenceRepository : IConferenceRepository
{
    private readonly List<Entities.Conference> _conferences = new();

    public Task<Entities.Conference> GetAsync(Guid id) =>
        Task.FromResult(_conferences.SingleOrDefault(x => x.ConferenceId == id));

    public async Task<IReadOnlyList<Entities.Conference>> GetAllAsync()
    {
        await Task.CompletedTask;
        return _conferences;
    }

    public Task AddAsync(Entities.Conference conference)
    {
        _conferences.Add(conference);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Entities.Conference conference)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Entities.Conference conference)
    {
        _conferences.Remove(conference);
        return Task.CompletedTask;
    }
}