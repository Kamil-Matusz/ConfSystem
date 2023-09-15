using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Conferences.Api.Controllers;

[ApiController]
internal class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}