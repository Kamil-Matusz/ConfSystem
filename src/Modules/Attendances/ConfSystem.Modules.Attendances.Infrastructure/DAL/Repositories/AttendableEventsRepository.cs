using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Domain.Repositories;
using ConfSystem.Modules.Attendances.Domain.Types;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Attendances.Infrastructure.DAL.Repositories;

internal class AttendableEventsRepository : IAttendableEventsRepository
{
    private readonly AttendancesDbContext _attendancesDbContext;
    private readonly DbSet<AttendableEvent> _attendableEvents;

    public AttendableEventsRepository(AttendancesDbContext attendancesDbContext)
    {
        _attendancesDbContext = attendancesDbContext;
        _attendableEvents = _attendancesDbContext.AttendableEvents;
    }

    public Task<AttendableEvent> GetAttendableEventAsync(AttendableEventId id)
        => _attendableEvents
            .Include(x => x.Slots)
            .SingleOrDefaultAsync(s => s.Id == id);

    public async Task AddAttendableEventAsync(AttendableEvent attendableEvent)
    {
        await _attendableEvents.AddAsync(attendableEvent);
        await _attendancesDbContext.SaveChangesAsync();
    }

    public async Task UpdateAttendableEventAsync(AttendableEvent attendableEvent)
    {
        _attendableEvents.Update(attendableEvent);
        await _attendancesDbContext.SaveChangesAsync();
    }
}