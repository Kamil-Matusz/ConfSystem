using ConfSystem.Shared.Abstractions.Kernel.Types;

namespace ConfSystem.Modules.Agendas.Domain.Submissions.Entities;

public class Speaker : AggregateRoot
{
    public string FullName { get; init; }
    // many to many EF relations
    public IEnumerable<Submission> Submissions => _submissions;

    private ICollection<Submission> _submissions;

    public Speaker(AggregateId id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }

    public static Speaker Create(Guid id, string fullName)
        => new Speaker(id, fullName);
}