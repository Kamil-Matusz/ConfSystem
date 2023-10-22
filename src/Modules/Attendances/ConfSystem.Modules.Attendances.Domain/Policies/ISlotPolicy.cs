using ConfSystem.Modules.Attendances.Domain.Entities;

namespace ConfSystem.Modules.Attendances.Domain.Policies;

public interface ISlotPolicy
{
    IEnumerable<Slot> Generate(int participantsLimit);
}