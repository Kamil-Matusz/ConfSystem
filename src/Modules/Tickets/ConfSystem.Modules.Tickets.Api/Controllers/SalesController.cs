using ConfSystem.Modules.Tickets.Core.DTO;
using ConfSystem.Modules.Tickets.Core.Services.TicketsSale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Tickets.Api.Controllers;

internal class SalesController : BaseController
{
    private readonly ITicketSaleService _ticketSaleService;
    private const string Policy = "tickets";

    public SalesController(ITicketSaleService ticketSaleService)
    {
        _ticketSaleService = ticketSaleService;
    }
    
    [HttpGet("conferences/{conferenceId}")]
    public async Task<ActionResult<IEnumerable<TicketSaleInfoDto>>> GetAll(Guid conferenceId)
        => OkOrNotFound(await _ticketSaleService.GetAllAsync(conferenceId));

    [HttpGet("conferences/{conferenceId}/current")]
    public async Task<ActionResult<TicketSaleInfoDto>> GetCurrent(Guid conferenceId)
        => OkOrNotFound(await _ticketSaleService.GetCurrentAsync(conferenceId));

    [Authorize]
    [HttpPost("conferences/{conferenceId}")]
    public async Task<ActionResult> Post(Guid conferenceId, TicketSaleDto dto)
    {
        dto.ConferenceId = conferenceId;
        await _ticketSaleService.AddAsync(dto);
        return NoContent();
    }
}