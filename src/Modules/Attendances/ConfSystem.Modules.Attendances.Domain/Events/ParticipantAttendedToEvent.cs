using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Shared.Abstractions.Kernel;

namespace ConfSystem.Modules.Attendances.Domain.Events;

public record ParticipantAttendedToEvent(Participant Participant, Attendance Attendance) : IDomainEvent;