using ConfSystem.Services.Tickets.Core.Entities;

namespace ConfSystem.Services.Tickets.Core.DAL.Repositories.Tickets;

internal interface ITicketRepository
{
    Task<Ticket> GetTicketAsync(Guid conferenceId, Guid userId);
    Task<int> CountTicketsForConferenceAsync(Guid conferenceId);
    Task<IReadOnlyList<Ticket>> GetTicketsForUserAsync(Guid userId);
    Task AddTicketAsync(Ticket ticket);
    Task AddManyTicketsAsync(IEnumerable<Ticket> ticket);
    Task UpdateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(Ticket ticket);
}