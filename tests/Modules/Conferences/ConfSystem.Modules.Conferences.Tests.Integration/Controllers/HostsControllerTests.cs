using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ConfSystem.Modules.Conferences.Core.DAL;
using ConfSystem.Modules.Conferences.Core.DTO;
using ConfSystem.Modules.Conferences.Core.Entities;
using ConfSystem.Modules.Conferences.Tests.Integration.Common;
using ConfSystem.Shared.Tests;
using Shouldly;
using Xunit;

namespace ConfSystem.Modules.Conferences.Tests.Integration.Controllers;

[Collection("integration")]
public class HostsControllerTests : IClassFixture<TestApplicationFactory>, IClassFixture<TestConferencesDbContext>
{
    private const string Path = "conferences-module/host";
    private readonly HttpClient _client;
    private readonly ConferencesDbContext _dbContext;
    private void Authenticate(Guid userId)
    {
        var jwt = AuthHelper.GenerateJwt(userId.ToString());
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }
    
    public HostsControllerTests(TestApplicationFactory factory, TestConferencesDbContext dbContext)
    {
        _client = factory
            .CreateClient();
        _dbContext = dbContext.DbContext;
    }
    
    [Fact]
    public async Task get_browse_hosts__should_return_ok_status_code()
    {
        // Act
        var response = await _client.GetAsync($"{Path}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task added_new_host__should_return_ok_status_code()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var hostId = Guid.NewGuid();
        var name = "Host #1";
        var description = "First host";
        var hostDto = new HostDto
        {
            HostId = hostId,
            Name = name,
            Description = description
        };
        Authenticate(userId);
        
        // Act
        var response = await _client.PostAsJsonAsync($"{Path}", hostDto);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task deleted_host_should_return_no_content_status_code()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var hostId = Guid.NewGuid();
        var name = "Host #1";
        var description = "First host";
        var host = new Host
        {
            HostId = hostId,
            Name = name,
            Description = description
        };
        
        await _dbContext.Hosts.AddAsync(host);
        await _dbContext.SaveChangesAsync();
        
        Authenticate(userId);

        // Act
        var deleteResponse = await _client.DeleteAsync($"{Path}/{hostId}");

        // Assert
        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        
    }

}