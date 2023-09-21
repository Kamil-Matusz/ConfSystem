using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Speakers.Api.Controllers;

[ApiController]
[Route(BasePath + "/[controller]")]
public class BaseController : ControllerBase
{
    protected const string BasePath = "speakers-module";
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}