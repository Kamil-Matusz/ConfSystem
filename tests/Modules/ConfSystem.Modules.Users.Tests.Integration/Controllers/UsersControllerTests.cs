using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ConfSystem.Modules.Users.Core.DAL;
using ConfSystem.Modules.Users.Core.DTO;
using ConfSystem.Modules.Users.Core.Entities;
using ConfSystem.Modules.Users.Tests.Integration.Common;
using ConfSystem.Shared.Tests;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using Xunit;

namespace ConfSystem.Modules.Users.Tests.Integration.Controllers;

[Collection("integration")]
public class UsersControllerTests : IClassFixture<TestApplicationFactory>, IClassFixture<TestUsersDbContext>
{
    private const string Path = "users-module/Account";
    private readonly HttpClient _client;
    private readonly UsersDbContext _dbContext;
    private void Authenticate(Guid userId)
    {
        var jwt = AuthHelper.GenerateJwt(userId.ToString());
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }
    
    public UsersControllerTests(TestApplicationFactory factory, TestUsersDbContext dbContext)
    {
        _client = factory.CreateClient();
        _dbContext = dbContext.DbContext;
    }
    
    [Fact]
    public async Task post_sign_up_should_return_ok_status_code()
    {
        // Arrange
        var signUpDto = new SignUpDto
        {
            UserId = Guid.NewGuid(),
            Email = "test@example.com",
            Password = "password123",
            Role = "User",
            Claims = new Dictionary<string, IEnumerable<string>>()
        };
        
        // Act
        var response = await _client.PostAsJsonAsync($"{Path}/sign-up", signUpDto);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}