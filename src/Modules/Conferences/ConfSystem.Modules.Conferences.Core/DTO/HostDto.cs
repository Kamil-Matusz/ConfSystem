using System.ComponentModel.DataAnnotations;

namespace ConfSystem.Modules.Conferences.Core.DTO;

public class HostDto
{
    public Guid HostId { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }
    [StringLength(1000, MinimumLength = 3)]
    public string Description { get; set; }
}

internal class HostDetailsDto : HostDto
{
    public List<ConferenceDto> Conferences { get; set; }
}