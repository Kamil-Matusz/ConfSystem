using ConfSystem.Modules.Conferences.Core.Entities;

namespace ConfSystem.Modules.Conferences.Core.Policies;

public interface IConferenceDeletionPolicy
{
    Task<bool> CanDeleteAsync(Conference conference);
}