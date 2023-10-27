using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Agendas.Application.CallForPapers.Events;

internal record CallForPapersOpened(Guid ConferenceId, DateTime From, DateTime To) : IEvent;