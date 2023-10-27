using ConfSystem.Modules.Attendances.Domain.Entities;

namespace ConfSystem.Modules.Attendances.Domain.Policies;

public class RegularSlotPolicy : ISlotPolicy
{
    public IEnumerable<Slot> Generate(int participantsLimit)
        => Enumerable.Range(0, participantsLimit).Select(x => new Slot(Guid.NewGuid()));
}