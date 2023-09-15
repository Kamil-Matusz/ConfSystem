using ConfSystem.Modules.Conferences.Core.Entities;
using ConfSystem.Shared.Abstractions;

namespace ConfSystem.Modules.Conferences.Core.Policies;

public class ConferenceDeletionPolicy : IConferenceDeletionPolicy
{
    private readonly IClock _clock;

    public ConferenceDeletionPolicy(IClock clock)
    {
        _clock = clock;
    }
    public Task<bool> CanDeleteAsync(Conference conference)
    {
        var canDelete = _clock.CurrentDate().Date.AddDays(7) < conference.From.Date;
        return Task.FromResult(canDelete);
    }
}