using ConfSystem.Modules.Conferences.Core.Entities;

namespace ConfSystem.Modules.Conferences.Core.Repositories;

internal class InMemoryHostRepository : IHostRepository
{
    private readonly List<Host> _hosts = new();
    public Task<Host> GetAsync(Guid id) => Task.FromResult(_hosts.SingleOrDefault(X => X.HostId == id));

    public async Task<IReadOnlyList<Host>> GetAllAsync()
    {
        await Task.CompletedTask;
        return _hosts;
    }

    public Task AddAsync(Host host)
    {
        _hosts.Add(host);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Host host)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Host host)
    {
        _hosts.Remove(host);
        return Task.CompletedTask;
    }
}