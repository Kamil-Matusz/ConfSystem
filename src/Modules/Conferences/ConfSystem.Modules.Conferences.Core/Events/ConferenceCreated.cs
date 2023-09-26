using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Conferences.Core.Events;

public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;