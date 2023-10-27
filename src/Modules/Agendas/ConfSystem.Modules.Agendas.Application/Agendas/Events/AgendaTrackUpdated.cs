using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.Agendas.Events;

public record AgendaTrackUpdated(Guid Id) : IEvent;