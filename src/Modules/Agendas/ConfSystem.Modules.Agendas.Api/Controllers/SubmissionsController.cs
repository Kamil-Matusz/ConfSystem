using ConfSystem.Modules.Agendas.Application.Submissions.Commands;
using ConfSystem.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Agendas.Api.Controllers;

internal class SubmissionsController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;

    public SubmissionsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> CreateSubmissionAsync(CreateSubmission command)
    {
        await _commandDispatcher.SendAsync(command);
        return CreatedAtAction("Get", new { id = command.Id }, null);
    }
    
    [HttpPut("{id:guid}/approve")]
    public async Task<ActionResult> ApproveSubmissionAsync(Guid id)
    {
        await _commandDispatcher.SendAsync(new ApproveSubmission(id));
        return NoContent();
    }
    
    [HttpPut("{id:guid}/reject")]
    public async Task<ActionResult> RejectSubmissionAsync(Guid id)
    {
        await _commandDispatcher.SendAsync(new RejectSubmission(id));
        return NoContent();
    }
}