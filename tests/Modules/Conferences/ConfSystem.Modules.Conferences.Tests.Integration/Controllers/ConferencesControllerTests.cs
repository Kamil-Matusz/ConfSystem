using System.Net;
using System.Net.Http.Headers;
using ConfSystem.Modules.Conferences.Core.DAL;
using ConfSystem.Modules.Conferences.Tests.Integration.Common;
using ConfSystem.Shared.Tests;
using Shouldly;
using Xunit;

namespace ConfSystem.Modules.Conferences.Tests.Integration.Controllers;

public class ConferencesControllerTests : IClassFixture<TestApplicationFactory>, IClassFixture<TestConferencesDbContext>
{
    private const string Path = "conferences-module/conferences";
    private readonly HttpClient _client;
    private readonly ConferencesDbContext _dbContext;
    private void Authenticate(Guid userId)
    {
        var jwt = AuthHelper.GenerateJwt(userId.ToString());
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }

    public ConferencesControllerTests(TestApplicationFactory factory, TestConferencesDbContext dbContext)
    {
        _client = factory
            .CreateClient();
        _dbContext = dbContext.DbContext;
    }
    
    [Fact]
    public async Task get_browse_conferences__should_return_ok_status_code()
    {
        // Act
        var response = await _client.GetAsync($"{Path}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task get_browse_conference_with_id__should_return_ok_or_notfound_status_code()
    {
        // Arrange
        var conferenceId = Guid.NewGuid();
        
        // Act
        var response = await _client.GetAsync($"{Path}/{conferenceId}");
        
        // Assert
        response.StatusCode.ShouldBeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
    }
}