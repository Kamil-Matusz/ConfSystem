using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Attendances.Domain.Types;

public class SlotId : TypeId
{
    public SlotId(Guid value) : base(value)
    {
    }
        
    public static implicit operator SlotId(Guid id) => new(id);
}