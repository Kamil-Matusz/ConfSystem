using ConfSystem.Modules.Tickets.Core.DAL.Repositories.Conferences;
using ConfSystem.Modules.Tickets.Core.DAL.Repositories.Tickets;
using ConfSystem.Modules.Tickets.Core.DAL.Repositories.TicketsSale;
using ConfSystem.Modules.Tickets.Core.DTO;
using ConfSystem.Modules.Tickets.Core.Entities;
using ConfSystem.Modules.Tickets.Core.Events;
using ConfSystem.Modules.Tickets.Core.Exceptions;
using ConfSystem.Modules.Tickets.Core.Services.Conferences;
using ConfSystem.Shared.Abstractions;
using ConfSystem.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace ConfSystem.Modules.Tickets.Core.Services.Tickets;

internal class TicketService : ITicketService
{
    private readonly IClock _clock;
    private readonly IConferenceRepository _conferenceRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketSaleRepository _ticketSaleRepository;
    private readonly ITicketGenerator _generator;
    private readonly IMessageBroker _messageBroker;

    public TicketService(IClock clock, IConferenceRepository conferenceRepository, ITicketRepository ticketRepository, ITicketSaleRepository ticketSaleRepository, ITicketGenerator generator, IMessageBroker messageBroker)
    {
        _clock = clock;
        _conferenceRepository = conferenceRepository;
        _ticketRepository = ticketRepository;
        _ticketSaleRepository = ticketSaleRepository;
        _generator = generator;
        _messageBroker = messageBroker;
    }

    public async Task PurchaseAsync(Guid conferenceId, Guid userId)
    {
        var conference = await _conferenceRepository.GetConferenceAsync(conferenceId);
        if (conference is null)
        {
            throw new ConferenceNotFoundException(conferenceId);
        }

        var ticket = await _ticketRepository.GetTicketAsync(conferenceId, userId);
        if (ticket is not null)
        {
            throw new TicketAlreadyPurchasedException(conferenceId, userId);
        }

        var now = _clock.CurrentDate();
        var ticketSale = await _ticketSaleRepository.GetCurrentForConferenceAsync(conferenceId, now);
        if (ticketSale is null)
        {
            throw new TicketSaleUnavailableException(conferenceId);
        }
            
        if (ticketSale.Amount.HasValue)
        {
            await PurchaseAvailableAsync(ticketSale, userId, ticketSale.Price);
            return;
        }

        ticket = _generator.Generate(conferenceId, ticketSale.Id, ticketSale.Price);
        ticket.Purchase(userId, _clock.CurrentDate(), ticketSale.Price);
        await _ticketRepository.AddTicketAsync(ticket);
        await _messageBroker.PublishAsync(new TicketPurchased(ticket.Id, conferenceId, userId));
    }

    public async Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId)
    {
        var tickets = await _ticketRepository.GetTicketsForUserAsync(userId);

        return tickets.Select(x => new TicketDto(x.Code, x.Price, x.PurchasedAt.Value,
                new ConferenceDto(x.ConferenceId, x.Conference.Name)))
            .OrderBy(x => x.PurchasedAt);
    }
    
    private async Task PurchaseAvailableAsync(TicketSale ticketSale, Guid userId, decimal? price)
    {
        var conferenceId = ticketSale.ConferenceId;
        var ticket = ticketSale.Tickets.Where(x => x.UserId is null).OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
        if (ticket is null)
        {
            throw new TicketsUnavailableException(conferenceId);
        }

        ticket.Purchase(userId, _clock.CurrentDate(), price);
        await _ticketRepository.UpdateTicketAsync(ticket);
    }
}