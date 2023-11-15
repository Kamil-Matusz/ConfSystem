using ConfSystem.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Services.Tickets.Api.Controllers;

[ApiController]
[ProducesDefaultContentType]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    protected const string BasePath = "tickets-module";
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}