using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Attendances.Domain.Types;

public class UserId : TypeId
{
    public UserId(Guid value) : base(value)
    {
    }
        
    public static implicit operator UserId(Guid id) => new(id);
}