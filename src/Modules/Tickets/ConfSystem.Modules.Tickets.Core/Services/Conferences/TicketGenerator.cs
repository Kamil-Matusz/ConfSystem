using ConfSystem.Modules.Tickets.Core.Entities;
using ConfSystem.Shared.Abstractions;

namespace ConfSystem.Modules.Tickets.Core.Services.Conferences;

internal class TicketGenerator : ITicketGenerator
{
    private readonly IClock _clock;

    public TicketGenerator(IClock clock)
    {
        _clock = clock;
    }

    public Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price)
        => new()
        {
            Id = Guid.NewGuid(),
            TicketSaleId =  ticketSaleId,
            ConferenceId = conferenceId,
            Code = Guid.NewGuid().ToString("N"),
            Price = price,
            CreatedAt = _clock.CurrentDate()
        };
}