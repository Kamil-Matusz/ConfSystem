namespace ConfSystem.Modules.Tickets.Core.DTO;

public record TicketDto(string Code, decimal? Price, DateTime PurchasedAt, ConferenceDto Conference);