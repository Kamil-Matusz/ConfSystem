using ConfSystem.Modules.Attendances.Application.Clients.Agendas;
using ConfSystem.Modules.Attendances.Application.Clients.Agendas.DTO;
using ConfSystem.Modules.Attendances.Application.Events;
using ConfSystem.Modules.Attendances.Application.Events.Handlers;
using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Domain.Exceptions;
using ConfSystem.Modules.Attendances.Domain.Policies;
using ConfSystem.Modules.Attendances.Domain.Repositories;
using ConfSystem.Shared.Abstractions.Events;
using NSubstitute;
using Shouldly;
using Xunit;

namespace ConfSystem.Modules.Attandences.Tests.Unit.Events.Handlers;

public class AgendaItemAssignedToAgendaSlotHandlerTests
{
    private Task Act(AgendaItemAssignedToAgendaSlot @event) => _handler.HandleAsync(@event);
    
    private readonly IAttendableEventsRepository _attendableEventsRepository;
    private readonly IAgendasApiClient _agendasApiClient;
    private readonly ISlotPolicyFactory _slotPolicyFactory;
    private readonly ISlotPolicy _slotPolicy;
    private readonly IEventHandler<AgendaItemAssignedToAgendaSlot> _handler;

    public AgendaItemAssignedToAgendaSlotHandlerTests()
    {
        _attendableEventsRepository = Substitute.For<IAttendableEventsRepository>();
        _agendasApiClient = Substitute.For<IAgendasApiClient>();
        _slotPolicyFactory = Substitute.For<ISlotPolicyFactory>();
        _slotPolicy = Substitute.For<ISlotPolicy>();
        _handler = new AgendaItemAssignedToAgendaSlotHandler(_attendableEventsRepository, _agendasApiClient,
            _slotPolicyFactory);
    }
    
    private static AttendableEvent GetAttendableEvent()
        => new(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

    [Fact]
    public async Task given_already_existing_attendable_event_new_one_should_not_be_added()
    {
        // Arrange
        var attendableEvent = GetAttendableEvent();
        var @event = new AgendaItemAssignedToAgendaSlot(Guid.NewGuid(), attendableEvent.Id);
        _attendableEventsRepository.GetAttendableEventAsync(@event.AgendaItemId).Returns(attendableEvent);
        
        // Act
        await Act(@event);
        
        // Assert
        await _attendableEventsRepository.Received(1).GetAttendableEventAsync(@event.AgendaItemId);
        await _agendasApiClient.DidNotReceiveWithAnyArgs().GetRegularAgendaSlotAsync(default);
        await _attendableEventsRepository.DidNotReceiveWithAnyArgs().AddAttendableEventAsync(default);
    }

    [Fact]
    public async Task given_missing_regular_agenda_slot_handler_should_fail()
    {
        // Arrange
        var @event = new AgendaItemAssignedToAgendaSlot(Guid.NewGuid(), Guid.NewGuid());
        
        // Act
        var exception = await Record.ExceptionAsync(() => Act(@event));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AttendableEventNotFoundException>();
        await _attendableEventsRepository.Received(1).GetAttendableEventAsync(@event.AgendaItemId);
        await _agendasApiClient.Received(1).GetRegularAgendaSlotAsync(@event.AgendaItemId);
    }

    [Fact]
    public async Task given_no_participants_limit_attendable_event_should_not_be_added()
    {
        // Arrange
        var @event = new AgendaItemAssignedToAgendaSlot(Guid.NewGuid(), Guid.NewGuid());
        var agendaSlotDto = GetRegularAgendaSlotDto();
        _agendasApiClient.GetRegularAgendaSlotAsync(@event.AgendaItemId).Returns(agendaSlotDto);

        // Act
        await Act(@event);
        
        // Assert
        await _attendableEventsRepository.Received(1).GetAttendableEventAsync(@event.AgendaItemId);
        await _agendasApiClient.Received(1).GetRegularAgendaSlotAsync(@event.AgendaItemId);
        await _attendableEventsRepository.DidNotReceiveWithAnyArgs().AddAttendableEventAsync(default);
    }

    [Fact]
    public async Task given_participants_limit_attendable_event_should_be_added()
    {
        // Assert
        const int participantsLimit = 100;
        var slots = Enumerable.Range(0, participantsLimit).Select(x => new Slot(Guid.NewGuid()));
        var @event = new AgendaItemAssignedToAgendaSlot(Guid.NewGuid(), Guid.NewGuid());
        var agendaSlotDto = GetRegularAgendaSlotDto(participantsLimit);
        var tags = agendaSlotDto.AgendaItem.Tags.ToArray();
        _agendasApiClient.GetRegularAgendaSlotAsync(@event.AgendaItemId).Returns(agendaSlotDto);
        _slotPolicyFactory.Get(tags).Returns(_slotPolicy);
        _slotPolicy.Generate(participantsLimit).Returns(slots);
        
        // Act
        await Act(@event);
        
        // Assert
        await _attendableEventsRepository.Received(1).GetAttendableEventAsync(@event.AgendaItemId);
        await _agendasApiClient.Received(1).GetRegularAgendaSlotAsync(@event.AgendaItemId);
        _slotPolicyFactory.Received(1).Get(tags);
        _slotPolicy.Received(1).Generate(participantsLimit);
        await _attendableEventsRepository.Received(1).AddAttendableEventAsync(Arg.Is<AttendableEvent>(x => 
            x.Id == @event.AgendaItemId && x.ConferenceId == agendaSlotDto.AgendaItem.ConferenceId &&
            x.From == agendaSlotDto.From && x.To == agendaSlotDto.To));
    }
    

    private static RegularAgendaSlotDto GetRegularAgendaSlotDto(int? participantsLimit = null)
        => new()
        {
            Id = Guid.NewGuid(),
            ParticipantsLimit = participantsLimit,
            From = DateTime.UtcNow,
            To = DateTime.UtcNow.AddDays(1),
            AgendaItem = new AgendaItemDto
            {
                ConferenceId = Guid.NewGuid(),
                Tags = new[] {"tag1", "tag2"}
            }
        };
}