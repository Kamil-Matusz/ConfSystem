using System.ComponentModel.DataAnnotations;

namespace ConfSystem.Modules.Users.Core.DTO;

public class SignUpDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
}