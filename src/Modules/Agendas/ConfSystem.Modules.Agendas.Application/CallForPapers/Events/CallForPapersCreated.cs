using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.CallForPapers.Events;

internal record CallForPapersCreated(Guid ConferenceId) : IEvent;