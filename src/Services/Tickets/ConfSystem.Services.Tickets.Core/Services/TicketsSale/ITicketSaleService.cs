using ConfSystem.Services.Tickets.Core.DTO;

namespace ConfSystem.Services.Tickets.Core.Services.TicketsSale;

public interface ITicketSaleService
{
    Task AddAsync(TicketSaleDto dto);
    Task<IEnumerable<TicketSaleInfoDto>> GetAllAsync(Guid conferenceId);
    Task<TicketSaleInfoDto> GetCurrentAsync(Guid conferenceId);
}