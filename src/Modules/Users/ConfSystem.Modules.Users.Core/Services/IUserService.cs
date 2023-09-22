using ConfSystem.Modules.Users.Core.DTO;
using ConfSystem.Shared.Abstractions.Auth;

namespace ConfSystem.Modules.Users.Core.Services;

public interface IUserService
{
    Task<AccountDto> GetUserAsync(Guid id);
    Task<JsonWebToken> SignInAsync(SignInDto dto);
    Task SignUpAsync(SignUpDto dto);
}