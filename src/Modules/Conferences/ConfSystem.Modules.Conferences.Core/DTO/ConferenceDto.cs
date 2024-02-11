using System.ComponentModel.DataAnnotations;

namespace ConfSystem.Modules.Conferences.Core.DTO;

public class ConferenceDto
{
    public Guid ConferenceId { get; set; }
    public Guid HostId { get; set; }
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