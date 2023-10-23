using ConfSystem.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Attendances.Api.Controllers;

[ApiController]
[ProducesDefaultContentType]
[Route(BasePath + "/[controller]")]
public class BaseController : ControllerBase
{
    protected const string BasePath = "attendances-module";
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
    
    protected void AddResourceIdHeader(Guid id) => Response.Headers.Add("Resource-ID", id.ToString());
}