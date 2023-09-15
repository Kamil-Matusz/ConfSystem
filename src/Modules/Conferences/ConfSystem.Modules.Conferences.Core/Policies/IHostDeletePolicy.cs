using ConfSystem.Modules.Conferences.Core.Entities;

namespace ConfSystem.Modules.Conferences.Core.Policies;

internal interface IHostDeletePolicy
{
    Task<bool> CanDeleteAsync(Host host);
}