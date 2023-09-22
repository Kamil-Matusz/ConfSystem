using ConfSystem.Modules.Users.Core.DTO;
using ConfSystem.Modules.Users.Core.Services;
using ConfSystem.Shared.Abstractions.Auth;
using ConfSystem.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Users.Api.Controllers;

internal class AccountController : BaseController
{
    private readonly IUserService _userService;
    private readonly IContext _context;

    public AccountController(IUserService userService, IContext context)
    {
        _userService = userService;
        _context = context;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<AccountDto>> GetAsync()
        => OkOrNotFound(await _userService.GetUserAsync(_context.Identity.Id));

    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUpAsync(SignUpDto dto)
    {
        await _userService.SignUpAsync(dto);
        return NoContent();
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<JsonWebToken>> SignInAsync(SignInDto dto)
        => Ok(await _userService.SignInAsync(dto));
}