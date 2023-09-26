using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Conferences.Messages.Events;

public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;