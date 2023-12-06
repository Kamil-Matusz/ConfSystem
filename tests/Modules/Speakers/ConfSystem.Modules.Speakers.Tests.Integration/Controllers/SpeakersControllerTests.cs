using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ConfSystem.Modules.Agendas.Application.Agendas.DTO;
using ConfSystem.Modules.Speakers.Core.DAL;
using ConfSystem.Modules.Speakers.Core.DTO;
using ConfSystem.Modules.Speakers.Core.Entities;
using ConfSystem.Modules.Speakers.Tests.Integration.Common;
using ConfSystem.Shared.Tests;
using Shouldly;
using Xunit;
using SpeakerDto = ConfSystem.Modules.Speakers.Core.DTO.SpeakerDto;

namespace ConfSystem.Modules.Speakers.Tests.Integration.Controllers;

public class SpeakersControllerTests : IClassFixture<TestApplicationFactory>, IClassFixture<TestSpeakersDbContext>
{
    private const string Path = "speakers-module/speakers";
    private readonly HttpClient _client;
    private readonly SpeakersDbContext _dbContext;
    private void Authenticate(Guid userId)
    {
        var jwt = AuthHelper.GenerateJwt(userId.ToString());
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }
    
    public SpeakersControllerTests(TestApplicationFactory factory, TestSpeakersDbContext dbContext)
    {
        _client = factory.CreateClient();
        _dbContext = dbContext.DbContext;
    }
    
    [Fact]
    public async Task get_browse_speakers_should_return_ok_status_code()
    {
        // Act
        var response = await _client.GetAsync($"{Path}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task added_new_speaker_should_return_ok_status_code()
    {
        // Arrange
        var speakerDto = new SpeakerDto
        {
            SpeakerId = Guid.NewGuid(),
            Email = "test@example.com",
            FullName = "John Doe",
            Bio = "Test Bio",
            AvatarUrl = "http://example.com/avatar.jpg"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync($"{Path}", speakerDto);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task get_speaker_with_id_should_return_ok_status_code()
    {
        // Arrange
        var speakerId = Guid.NewGuid();
        var speaker = new Speaker
        {
            SpeakerId = speakerId,
            Email = "test@example.com",
            FullName = "John Doe",
            Bio = "Test Bio",
            AvatarUrl = "http://example.com/avatar.jpg"
        };

        await _dbContext.Speakers.AddAsync(speaker);
        await _dbContext.SaveChangesAsync();
        
        // Act
        var response = await _client.GetAsync($"{Path}/{speakerId}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}