namespace ConfSystem.Services.Tickets.Core.DTO;

public record TicketDto(string Code, decimal? Price, DateTime PurchasedAt, ConferenceDto Conference);