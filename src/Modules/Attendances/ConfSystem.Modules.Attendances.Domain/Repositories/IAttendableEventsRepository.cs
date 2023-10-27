using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Domain.Types;

namespace ConfSystem.Modules.Attendances.Domain.Repositories;

public interface IAttendableEventsRepository
{
    Task<AttendableEvent> GetAttendableEventAsync(AttendableEventId id);
    Task AddAttendableEventAsync(AttendableEvent attendableEvent);
    Task UpdateAttendableEventAsync(AttendableEvent attendableEvent);
}