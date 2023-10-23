using ConfSystem.Modules.Attendances.Application.Clients.Agendas;
using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Domain.Exceptions;
using ConfSystem.Modules.Attendances.Domain.Policies;
using ConfSystem.Modules.Attendances.Domain.Repositories;
using ConfSystem.Shared.Abstractions.Events;

namespace ConfSystem.Modules.Attendances.Application.Events.Handlers;

internal sealed class AgendaItemAssignedToAgendaSlotHandler : IEventHandler<AgendaItemAssignedToAgendaSlot>
{
    private readonly IAttendableEventsRepository _attendableEventsRepository;
    private readonly IAgendasApiClient _agendasApiClient;
    private readonly ISlotPolicyFactory _slotPolicyFactory;

    public AgendaItemAssignedToAgendaSlotHandler(IAttendableEventsRepository attendableEventsRepository,
        IAgendasApiClient agendasApiClient, ISlotPolicyFactory slotPolicyFactory)
    {
        _attendableEventsRepository = attendableEventsRepository;
        _agendasApiClient = agendasApiClient;
        _slotPolicyFactory = slotPolicyFactory;
    }
        
    public async Task HandleAsync(AgendaItemAssignedToAgendaSlot @event)
    {
        var attendableEvent = await _attendableEventsRepository.GetAttendableEventAsync(@event.AgendaItemId);
        if (attendableEvent is not null)
        {
            return;
        }

        var slot = await _agendasApiClient.GetRegularAgendaSlotAsync(@event.AgendaItemId);
        if (slot is null)
        {
            throw new AttendableEventNotFoundException(@event.AgendaItemId);
        }

        if (!slot.ParticipantsLimit.HasValue)
        {
            return;
        }
            
        attendableEvent = new AttendableEvent(@event.AgendaItemId, slot.AgendaItem.ConferenceId, slot.From, slot.To);
        var slotPolicy = _slotPolicyFactory.Get(slot.AgendaItem.Tags.ToArray());
        var slots = slotPolicy.Generate(slot.ParticipantsLimit.Value);
        attendableEvent.AddSlots(slots);
        await _attendableEventsRepository.AddAttendableEventAsync(attendableEvent);
    }
}