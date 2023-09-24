using ConfSystem.Modules.Users.Core.DAL.Repositories;
using ConfSystem.Modules.Users.Core.DTO;
using ConfSystem.Modules.Users.Core.Entities;
using ConfSystem.Modules.Users.Core.Exceptions;
using ConfSystem.Shared.Abstractions;
using ConfSystem.Shared.Abstractions.Auth;
using Microsoft.AspNetCore.Identity;

namespace ConfSystem.Modules.Users.Core.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IAuthManager _authManager;
    private readonly IClock _clock;

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IAuthManager authManager, IClock clock)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _authManager = authManager;
        _clock = clock;
    }

    public async Task<AccountDto> GetUserAsync(Guid id)
    {
        var user = await _userRepository.GetUserAsync(id);

        return user is null
            ? null
            : new AccountDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Role = user.Role,
                Claims = user.Claims,
                CreatedAt = user.CreatedAt
            };
    }

    public Task<AccountDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<JsonWebToken> SignInAsync(SignInDto dto)
    {
        var user = await _userRepository.GetUserAsync(dto.Email.ToLowerInvariant());
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (_passwordHasher.VerifyHashedPassword(default, user.Password, dto.Password) ==
            PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialsException();
        }

        if (!user.IsActive)
        {
            throw new UserNotActiveException(user.UserId);
        }

        var jwt = _authManager.CreateToken(user.UserId.ToString(), user.Role, claims: user.Claims);
        jwt.Email = user.Email;

        return jwt;
    }

    public async Task SignUpAsync(SignUpDto dto)
    {
        dto.UserId = Guid.NewGuid();
        var email = dto.Email.ToLowerInvariant();
        var user = await _userRepository.GetUserAsync(email);
        if (user is not null)
        {
            throw new EmailInUseException();
        }

        var password = _passwordHasher.HashPassword(default, dto.Password);
        user = new User
        {
            UserId = dto.UserId,
            Email = email,
            Password = password,
            Role = dto.Role?.ToLowerInvariant() ?? "user",
            CreatedAt = _clock.CurrentDate(),
            IsActive = true,
            Claims = dto.Claims ?? new Dictionary<string, IEnumerable<string>>()
        };
        await _userRepository.AddUserAsync(user);
    }
}