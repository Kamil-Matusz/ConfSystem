using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Attendances.Application.Commands;

public record AttendEvent(Guid Id, Guid ParticipantId) : ICommand;