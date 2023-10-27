using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Domain.Repositories;
using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Attendances.Application.Events.Handlers;

internal sealed class TicketPurchasedHandler : IEventHandler<TicketPurchased>
{
    private readonly IParticipantsRepository _participantsRepository;

    public TicketPurchasedHandler(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }
        
    public async Task HandleAsync(TicketPurchased @event)
    {
        var participant = await _participantsRepository.GetParticipantsAsync(@event.ConferenceId, @event.UserId);
        if (participant is not null)
        {
            return;
        }

        participant = new Participant(Guid.NewGuid(), @event.ConferenceId, @event.UserId);
        await _participantsRepository.AddParticipantsAsync(participant);
    }
}