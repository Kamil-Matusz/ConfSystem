using ConfSystem.Shared.Abstractions.Events;
using Convey.MessageBrokers;
using IEvent = Convey.CQRS.Events.IEvent;

namespace ConfSystem.Services.Tickets.Core.Events.External;

[Message("modular-monolith")]
public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;