using ConfSystem.Modules.Attendances.Domain.Types;

namespace ConfSystem.Modules.Attendances.Domain.Entities;

public record Attendance(Guid Id, AttendableEventId AttendableEventId, SlotId SlotId, ParticipantId ParticipantId,
    DateTime From, DateTime To);