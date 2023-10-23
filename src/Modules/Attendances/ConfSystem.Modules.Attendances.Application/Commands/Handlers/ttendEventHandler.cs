using ConfSystem.Modules.Attendances.Domain.Exceptions;
using ConfSystem.Modules.Attendances.Domain.Repositories;
using ConfSystem.Shared.Abstractions.Commands;

namespace ConfSystem.Modules.Attendances.Application.Commands.Handlers;

internal sealed class AttendEventHandler : ICommandHandler<AttendEvent>
{
    private readonly IAttendableEventsRepository _attendableEventsRepository;
    private readonly IParticipantsRepository _participantsRepository;

    public AttendEventHandler(IAttendableEventsRepository attendableEventsRepository,
        IParticipantsRepository participantsRepository)
    {
        _attendableEventsRepository = attendableEventsRepository;
        _participantsRepository = participantsRepository;
    }
        
    public async Task HandleAsync(AttendEvent command)
    {
        var attendableEvent = await _attendableEventsRepository.GetAttendableEventAsync(command.Id);
        if (attendableEvent is null)
        {
            throw new AttendableEventNotFoundException(command.Id);
        }

        var participant = await _participantsRepository
            .GetParticipantsAsync(attendableEvent.ConferenceId, command.ParticipantId);
        if (participant is null)
        {
            throw new ParticipantNotFoundException(attendableEvent.ConferenceId, command.ParticipantId);
        }

        attendableEvent.Attend(participant);
        await _participantsRepository.UpdateParticipantsAsync(participant);
        await _attendableEventsRepository.UpdateAttendableEventAsync(attendableEvent);
    }
}