using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Tickets.Core.Events.External;

public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;