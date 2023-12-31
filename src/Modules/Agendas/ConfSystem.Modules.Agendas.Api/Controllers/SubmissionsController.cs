using ConfSystem.Modules.Agendas.Application.Submissions.Commands;
using ConfSystem.Modules.Agendas.Application.Submissions.DTO;
using ConfSystem.Modules.Agendas.Application.Submissions.Queries;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Agendas.Api.Controllers;

internal class SubmissionsController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public SubmissionsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<SubmissionDto>> GetAsync(Guid id)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetSubmission { Id = id }));
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> CreateSubmissionAsync(CreateSubmission command)
    {
        await _commandDispatcher.SendAsync(command);
        return CreatedAtAction("Get", new { id = command.Id }, null);
    }
    
    [HttpPut("{id:guid}/approve")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> ApproveSubmissionAsync(Guid id)
    {
        await _commandDispatcher.SendAsync(new ApproveSubmission(id));
        return NoContent();
    }
    
    [HttpPut("{id:guid}/reject")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> RejectSubmissionAsync(Guid id)
    {
        await _commandDispatcher.SendAsync(new RejectSubmission(id));
        return NoContent();
    }
}