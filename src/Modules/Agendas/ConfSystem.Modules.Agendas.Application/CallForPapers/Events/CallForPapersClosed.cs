using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.CallForPapers.Events;

internal record CallForPapersClosed(Guid ConferenceId) : IEvent;