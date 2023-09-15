using ConfSystem.Modules.Conferences.Core.Entities;

namespace ConfSystem.Modules.Conferences.Core.Policies;

internal class HostDeletePolicy : IHostDeletePolicy
{
    private readonly IConferenceDeletionPolicy _conferenceDeletionPolicy;

    public HostDeletePolicy(IConferenceDeletionPolicy conferenceDeletionPolicy)
    {
        _conferenceDeletionPolicy = conferenceDeletionPolicy;
    }
    
    public async Task<bool> CanDeleteAsync(Host host)
    {
        if (host.Conferences is null || !host.Conferences.Any())
        {
            return true;
        }

        foreach (var conference in host.Conferences)
        {
            if (await _conferenceDeletionPolicy.CanDeleteAsync(conference) is false)
            {
                return false;
            }
        }

        return true;
    }
}