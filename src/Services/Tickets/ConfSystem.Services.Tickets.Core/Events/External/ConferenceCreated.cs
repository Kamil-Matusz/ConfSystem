using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Services.Tickets.Core.Events.External;

public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;