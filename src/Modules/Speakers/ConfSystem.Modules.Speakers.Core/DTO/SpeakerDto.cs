using System.ComponentModel.DataAnnotations;

namespace ConfSystem.Modules.Speakers.Core.DTO;

public class SpeakerDto
{
    public Guid SpeakerId { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Bio { get; set; }
    public string AvatarUrl { get; set; }
}