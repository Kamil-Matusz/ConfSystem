using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace ConfSystem.Modules.Attandences.Tests.Unit.Entities;

public class AttendableEventTests
{
    private readonly AttendableEvent _attendableEvent;
    private readonly Guid _conferenceId = Guid.NewGuid();
    private Attendance Act(Participant participant) => _attendableEvent.Attend(participant);
    private Participant GetParticipant() => new(Guid.NewGuid(), _conferenceId, Guid.NewGuid());
    private static DateTime GetDate(int hour, int minute = 0, int second = 0)
        => new(2023, 11, 1, hour, minute, second);
    
    
    
    // Setup
    public AttendableEventTests()
    {
        _attendableEvent = new AttendableEvent(Guid.NewGuid(), _conferenceId,
            new DateTime(2023, 12, 1, 9, 0, 0),
            new DateTime(2023, 12, 1, 10, 0, 0));
    }
    
    [Fact]
    public void given_no_slots_attend_should_fail()
    {
        // Arrange
        var participant = GetParticipant();

        // Act
        var exception = Record.Exception(() => Act(participant));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<NoFreeSlotsException>();
        _attendableEvent.Slots.ShouldBeEmpty();
    }
    
    [Fact]
    public void given_existing_slot_with_same_participant_attend_should_fail()
    {
        // Arrange
        var participant = GetParticipant();
        _attendableEvent.AddSlots(new Slot(Guid.NewGuid(), participant.Id));

        // Act
        var exception = Record.Exception(() => Act(participant));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AlreadyParticipatingInEventException>();
    }

    [Fact]
    public void given_free_slots_attend_should_succeed()
    {
        // Arrange
        var participant = GetParticipant();
        _attendableEvent.AddSlots(new Slot(Guid.NewGuid()));
        
        // Act
        var attendance = Act(participant);

        // Assert
        attendance.ShouldNotBeNull();
        attendance.ParticipantId.ShouldBe(participant.Id);
    }
    
}