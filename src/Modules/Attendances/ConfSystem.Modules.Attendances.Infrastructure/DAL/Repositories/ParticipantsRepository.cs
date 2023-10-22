using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Domain.Events;
using ConfSystem.Modules.Attendances.Domain.Repositories;
using ConfSystem.Modules.Attendances.Domain.Types;
using ConfSystem.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ConfSystem.Modules.Attendances.Infrastructure.DAL.Repositories;

internal class ParticipantsRepository : IParticipantsRepository
{
    private readonly AttendancesDbContext _attendancesDbContext;
    private readonly DbSet<Participant> _participants;

    public ParticipantsRepository(AttendancesDbContext attendancesDbContext)
    {
        _attendancesDbContext = attendancesDbContext;
        _participants = _attendancesDbContext.Participants;
    }

    public Task<Participant> GetParticipantsAsync(ParticipantId id)
        => _participants
            .Include(x => x.Attendances)
            .SingleOrDefaultAsync(x => x.Id == id);

    public Task<Participant> GetParticipantsAsync(ConferenceId conferenceId, UserId userId)
        => _participants
            .Include(x => x.Attendances)
            .SingleOrDefaultAsync(x => x.ConferenceId == conferenceId && x.UserId == userId);

    public async Task AddParticipantsAsync(Participant participant)
    {
        await _participants.AddAsync(participant);
        await _attendancesDbContext.SaveChangesAsync();
    }

    public async Task UpdateParticipantsAsync(Participant participant)
    {
        var newAttendances = participant.Events
            .OfType<ParticipantAttendedToEvent>()
            .Select(x => x.Attendance);

        foreach (var attendance in newAttendances)
        {
            _attendancesDbContext.Entry(attendance).State = EntityState.Added;
        }
            
        _participants.Update(participant);
        await _attendancesDbContext.SaveChangesAsync();
    }
}