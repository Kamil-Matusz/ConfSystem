using System.ComponentModel.DataAnnotations;

namespace ConfSystem.Modules.Users.Core.DTO;

public class SignInDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}