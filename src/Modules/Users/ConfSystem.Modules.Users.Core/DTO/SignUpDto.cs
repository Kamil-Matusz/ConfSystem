using System.ComponentModel.DataAnnotations;

namespace ConfSystem.Modules.Users.Core.DTO;

public class SignUpDto
{
    public Guid UserId { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string Role { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
}