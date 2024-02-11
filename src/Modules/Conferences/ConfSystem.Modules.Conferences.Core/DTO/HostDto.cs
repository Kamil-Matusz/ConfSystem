using System.ComponentModel.DataAnnotations;

namespace ConfSystem.Modules.Conferences.Core.DTO;

public class HostDto
{
    public Guid HostId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

internal class HostDetailsDto : HostDto
{
    public List<ConferenceDto> Conferences { get; set; }
}