using ConfSystem.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Users.Api.Controllers;

[ApiController]
[ProducesDefaultContentType]
[Route(BasePath + "/[controller]")]
public class BaseController : ControllerBase
{
    protected const string BasePath = "users-module";
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}