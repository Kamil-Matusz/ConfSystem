using ConfSystem.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Agendas.Api.Controllers;

[ApiController]
[ProducesDefaultContentType]
[Route(BasePath + "/[controller]")]
internal class BaseController : ControllerBase
{
    protected const string BasePath = "agendas-module";
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