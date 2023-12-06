using ConfSystem.Modules.Agendas.Application.CallForPapers.Commands;
using ConfSystem.Modules.Agendas.Application.CallForPapers.DTO;
using ConfSystem.Modules.Agendas.Application.CallForPapers.Queries;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Queries;
using ConfSystem.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Agendas.Api.Controllers;

[Microsoft.AspNetCore.Components.Route(AgendasModule.BasePath + "/conferences/{conferenceId:guid}/cfp")]
[Authorize(Policy)]
internal class CallForPapersController : BaseController
{
    private const string Policy = "cfp";
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public CallForPapersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CallForPapersDto>> GetAsync(Guid conferenceId) 
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCallForPapers {ConferenceId = conferenceId}));

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> CreateAsync(Guid conferenceId, CreateCallForPapers command)
    {
        await _commandDispatcher.SendAsync(command.Bind(x => x.ConferenceId, conferenceId));
        return CreatedAtAction("Get", new {conferenceId = command.ConferenceId}, null);
    }

    [HttpPut("open")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> OpenAsync(Guid conferenceId)
    {
        await _commandDispatcher.SendAsync(new OpenCallForPapers(conferenceId));
        return NoContent();
    }
        
    [HttpPut("close")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> CloseAsync(Guid conferenceId)
    {
        await _commandDispatcher.SendAsync(new OpenCallForPapers(conferenceId));
        return NoContent();
    }
}