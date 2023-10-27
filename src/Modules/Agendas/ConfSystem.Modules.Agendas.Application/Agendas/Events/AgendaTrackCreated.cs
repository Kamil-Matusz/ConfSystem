using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Events;

public record AgendaTrackCreated(Guid Id) : IEvent;