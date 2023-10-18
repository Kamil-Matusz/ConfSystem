using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Agendas.Application.CallForPapers.Commands;

public record OpenCallForPapers(Guid ConferenceId) : ICommand;