namespace ConfSystem.Modules.Conferences.Core.Entities;

public class Host
{
    public Guid HostId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<Conference> Conferences { get; set; }
}