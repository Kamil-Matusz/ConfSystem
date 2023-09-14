using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Conferences.Api.Controllers;

[Route("conferences-module")]
internal class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => "Hello Conference";
}