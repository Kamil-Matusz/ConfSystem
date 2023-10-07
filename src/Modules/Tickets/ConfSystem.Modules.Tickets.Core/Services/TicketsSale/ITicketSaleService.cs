using ConfSystem.Modules.Tickets.Core.DTO;

namespace ConfSystem.Modules.Tickets.Core.Services.TicketsSale;

public interface ITicketSaleService
{
    Task AddAsync(TicketSaleDto dto);
    Task<IEnumerable<TicketSaleInfoDto>> GetAllAsync(Guid conferenceId);
    Task<TicketSaleInfoDto> GetCurrentAsync(Guid conferenceId);
}