using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ConfSystem.Modules.Attandences.Tests.Integration.Common;
using ConfSystem.Modules.Attendances.Application.Clients.Agendas;
using ConfSystem.Modules.Attendances.Application.DTO;
using ConfSystem.Modules.Attendances.Domain.Entities;
using ConfSystem.Modules.Attendances.Infrastructure.DAL;
using ConfSystem.Shared.Tests;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;
using AgendaItemDto = ConfSystem.Modules.Attendances.Application.Clients.Agendas.DTO.AgendaItemDto;
using AgendaTrackDto = ConfSystem.Modules.Attendances.Application.Clients.Agendas.DTO.AgendaTrackDto;
using RegularAgendaSlotDto = ConfSystem.Modules.Attendances.Application.Clients.Agendas.DTO.RegularAgendaSlotDto;

namespace ConfSystem.Modules.Attandences.Tests.Integration.Controllers;

public class AttendancesControllerTests : IClassFixture<TestApplicationFactory>, IClassFixture<TestAttendancesDbContext>
{
    private const string Path = "attendances-module/attendances";
    private readonly HttpClient _client;
    private readonly AttendancesDbContext _dbContext;
    private readonly IAgendasApiClient _agendasApiClient;
    private void Authenticate(Guid userId)
    {
        var jwt = AuthHelper.GenerateJwt(userId.ToString());
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }

    [Fact]
    public async Task get_browse_attendances_without_being_authorized_should_return_unauthorized_status_code()
    {
        // Arrange
        var conferenceId = Guid.NewGuid();
        
        // Act
        var response = await _client.GetAsync($"{Path}/{conferenceId}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task get_browse_attendances_given_invalid_participant_should_return_not_found()
    {
        // Arrange
        var conferenceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        Authenticate(userId);
        
        // Act
        var response = await _client.GetAsync($"{Path}/{conferenceId}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task get_browse_attendances_given_valid_conference_and_participant_should_return_all_attendances()
    {
            // Arrange
            var from = DateTime.UtcNow;
            var to = from.AddDays(1);
            var conferenceId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var participant = new Participant(Guid.NewGuid(), conferenceId, userId);
            var slot = new Slot(Guid.NewGuid(), participant.Id);
            var attendableEvent = new AttendableEvent(Guid.NewGuid(), conferenceId, from, to, new[] {slot});
            var attendance = new Attendance(Guid.NewGuid(), attendableEvent.Id, slot.Id, participant.Id, from, to);

            await _dbContext.AttendableEvents.AddAsync(attendableEvent);
            await _dbContext.Attendances.AddAsync(attendance);
            await _dbContext.Participants.AddAsync(participant);
            await _dbContext.Slots.AddAsync(slot);
            await _dbContext.SaveChangesAsync();

            _agendasApiClient.GetAgendaAsync(conferenceId).Returns(new List<AgendaTrackDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Track 1",
                    ConferenceId = conferenceId,
                    Slots = new []
                    {
                        new RegularAgendaSlotDto
                        {
                            Id = Guid.NewGuid(),
                            From = from,
                            To = to,
                            AgendaItem = new AgendaItemDto
                            {
                                Id = attendableEvent.Id,
                                Title = "test",
                                Description = "test",
                                Level = 1
                            }
                        }
                    }
                }
            });
            Authenticate(userId);
            
            // Act
            var response = await _client.GetAsync($"{Path}/{conferenceId}");
            response.IsSuccessStatusCode.ShouldBeTrue();

            // Arrange
            var attendances = await response.Content.ReadFromJsonAsync<AttendanceDto[]>();
            attendances.ShouldNotBeEmpty();
            attendances.Length.ShouldBe(1);
    }
        
    public AttendancesControllerTests(TestApplicationFactory factory, TestAttendancesDbContext dbContext)
    {
        _agendasApiClient = Substitute.For<IAgendasApiClient>();
        _client = factory
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.AddSingleton(_agendasApiClient);
            }))
            .CreateClient();
        _dbContext = dbContext.DbContext;
    }
}