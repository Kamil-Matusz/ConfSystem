using ConfSystem.Modules.Agendas.Application.Agendas.Commands;
using ConfSystem.Modules.Agendas.Application.Agendas.DTO;
using ConfSystem.Modules.Agendas.Application.Agendas.Queries;
using ConfSystem.Shared.Abstractions.Commands;
using ConfSystem.Shared.Abstractions.Queries;
using ConfSystem.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Agendas.Api.Controllers;

[Microsoft.AspNetCore.Components.Route(AgendasModule.BasePath + "/agendas/{conferenceId:guid}")]
[Authorize(Policy)]
internal class AgendasController : BaseController
{
    private const string Policy = "agendas";
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public AgendasController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AgendaTrackDto>>> GetAgendaAsync(Guid conferenceId) 
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetAgenda{ConferenceId = conferenceId}));
        
    [HttpGet("items")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AgendaItemDto>>> BrowseAgendaItemsAsync(Guid conferenceId) 
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new BrowseAgendaItems{ConferenceId = conferenceId}));
        
    [HttpGet("items/{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<AgendaItemDto>> GetAgendaItemAsync(Guid id) 
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetAgendaItem{Id = id}));

    [HttpPost("tracks")]
    public async Task<ActionResult> CreateAgendaTrackAsync(Guid conferenceId, CreateAgendaTrack command)
    {
        await _commandDispatcher.SendAsync(command.Bind(x => x.ConferenceId, conferenceId));
        AddResourceIdHeader(command.Id);
        return NoContent();
    }
        
    [HttpPost("slots")]
    public async Task<ActionResult> CreateAgendaSlotAsync(CreateAgendaSlot command)
    {
        await _commandDispatcher.SendAsync(command);
        AddResourceIdHeader(command.Id);
        return NoContent();
    }
        
    [HttpPut("slots/placeholder")]
    public async Task<ActionResult> AssignPlaceholderAgendaSlotAsync(AssignPlaceholderAgendaSlot command)
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
        
    [HttpPut("slots/regular")]
    public async Task<ActionResult> AssignRegularAgendaSlotAsync(AssignRegularAgendaSlot command)
    {
        await _commandDispatcher.SendAsync(command);
        return NoContent();
    }
}