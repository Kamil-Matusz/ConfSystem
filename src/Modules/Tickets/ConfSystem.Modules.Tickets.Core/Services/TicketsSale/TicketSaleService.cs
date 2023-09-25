using ConfSystem.Modules.Tickets.Core.DAL.Repositories.Conferences;
using ConfSystem.Modules.Tickets.Core.DAL.Repositories.Tickets;
using ConfSystem.Modules.Tickets.Core.DAL.Repositories.TicketsSale;
using ConfSystem.Modules.Tickets.Core.DTO;
using ConfSystem.Modules.Tickets.Core.Entities;
using ConfSystem.Modules.Tickets.Core.Exceptions;
using ConfSystem.Modules.Tickets.Core.Services.Conferences;
using ConfSystem.Shared.Abstractions;

namespace ConfSystem.Modules.Tickets.Core.Services.TicketsSale;

internal class TicketSaleService : ITicketSaleService
{
    private readonly IConferenceRepository _conferenceRepository;
    private readonly ITicketSaleRepository _ticketSaleRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketGenerator _ticketGenerator;
    private readonly IClock _clock;

    public TicketSaleService(IConferenceRepository conferenceRepository, ITicketSaleRepository ticketSaleRepository, ITicketRepository ticketRepository, ITicketGenerator ticketGenerator, IClock clock)
    {
        _conferenceRepository = conferenceRepository;
        _ticketSaleRepository = ticketSaleRepository;
        _ticketRepository = ticketRepository;
        _ticketGenerator = ticketGenerator;
        _clock = clock;
    }

    public async Task AddAsync(TicketSaleDto dto)
    {
        var conference = await _conferenceRepository.GetConferenceAsync(dto.ConferenceId);
        if (conference is null)
        {
            throw new ConferenceNotFoundException(dto.ConferenceId);
        }

        if (conference.ParticipantsLimit.HasValue)
        {
            var ticketsCount = await _ticketRepository.CountTicketsForConferenceAsync(conference.Id);
            if (ticketsCount + dto.Amount > conference.ParticipantsLimit)
            {
                throw new TooManyTicketsException(conference.Id);
            }
        }

        dto.Id = Guid.NewGuid();
        var ticketSale = new TicketSale
        {
            Id = dto.Id,
            ConferenceId = dto.ConferenceId,
            From = dto.From,
            To = dto.To,
            Amount = dto.Amount,
            Price = dto.Price,
            Name = dto.Name
        };
        await _ticketSaleRepository.AddAsync(ticketSale);

        if (ticketSale.Amount.HasValue)
        {
            var tickets = new List<Ticket>();
            for (var i = 0; i < ticketSale.Amount; i++)
            {
                var ticket = _ticketGenerator.Generate(conference.Id, ticketSale.Id, ticketSale.Price);
                tickets.Add(ticket);
            }

            await _ticketRepository.AddManyTicketsAsync(tickets);
        }
    }

    public async Task<IEnumerable<TicketSaleInfoDto>> GetAllAsync(Guid conferenceId)
    {
        var conference = await _conferenceRepository.GetConferenceAsync(conferenceId);
        if (conference is null)
        {
            return null;
        }

        var ticketSales = await _ticketSaleRepository.BrowseForConferenceAsync(conferenceId);

        return ticketSales.Select(x => Map(x, conference));
    }

    public async Task<TicketSaleInfoDto> GetCurrentAsync(Guid conferenceId)
    {
        var conference = await _conferenceRepository.GetConferenceAsync(conferenceId);
        if (conference is null)
        {
            return null;
        }

        var now = _clock.CurrentDate();
        var ticketSale = await _ticketSaleRepository.GetCurrentForConferenceAsync(conferenceId, now);
            
        return ticketSale is not null ? Map(ticketSale, conference) : null;
    }
    
    private static TicketSaleInfoDto Map(TicketSale ticketSale, Conference conference)
    {
        int? availableTickets = null;
        var totalTickets = ticketSale.Amount;
        if (totalTickets.HasValue)
        {
            availableTickets = ticketSale.Tickets.Count(x => x.UserId is null);
        }

        return new TicketSaleInfoDto( ticketSale.Name, new ConferenceDto(conference.Id, conference.Name), ticketSale.Price,
            totalTickets, availableTickets, ticketSale.From, ticketSale.To);
    }
}