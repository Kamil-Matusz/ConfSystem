using ConfSystem.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Conferences.Api.Controllers;

[ApiController]
[ProducesDefaultContentType]
[Route(BasePath + "/[controller]")]
internal class BaseController : ControllerBase
{
    protected const string BasePath = "conferences-module";
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}