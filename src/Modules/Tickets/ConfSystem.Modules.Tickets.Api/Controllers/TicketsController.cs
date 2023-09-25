using ConfSystem.Modules.Tickets.Core.DTO;
using ConfSystem.Modules.Tickets.Core.Services.Tickets;
using ConfSystem.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace ConfSystem.Modules.Tickets.Api.Controllers;

internal class TicketsController : BaseController
{
    private readonly ITicketService _ticketService;
    private readonly IContext _context;

    public TicketsController(ITicketService ticketService, IContext context)
    {
        _ticketService = ticketService;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TicketDto>>> Get()
        => Ok(await _ticketService.GetForUserAsync(_context.Identity.Id));

    [HttpPost("conferences/{conferenceId}/purchase")]
    public async Task<ActionResult> Purchase(Guid conferenceId)
    {
        await _ticketService.PurchaseAsync(conferenceId, _context.Identity.Id);
        return NoContent();
    }
}