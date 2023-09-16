using System.ComponentModel.DataAnnotations;

namespace ConfSystem.Modules.Conferences.Core.DTO;

public class ConferenceDto
{
    public Guid ConferenceId { get; set; }
    [Required]
    public Guid HostId { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }
    public string HostName { get; set; }
    public string Location { get; set; }
    public string LogoUrl { get; set; }
    public int? ParticipantsLimit { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}

internal class ConferenceDetailsDto : ConferenceDto
{
    [Required]
    public string Description { get; set; }
}