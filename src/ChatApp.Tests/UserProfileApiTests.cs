using ChatApp.Application.DTOs;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Xunit;

public class UserProfileApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    private readonly IConfiguration _configuration;


    public UserProfileApiTests(CustomWebApplicationFactory factory) 
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
    }

    [Fact]
    public async Task GetProfile_ShouldReturn_UserProfile()
    {
        var userId = Guid.NewGuid();
        using var scope = _factory.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ChatAppDbContext>();

        db.Users.Add(new User
        {
            Id = userId,
            Username = "testuser",
            Email = "test@example.com",
            PasswordHash = "hashed_password",
            Bio = "This is a test bio",
            ProfilePictureUrl = "test.jpg"
        });

        await db.SaveChangesAsync();

        var token =  JwtTokenHelper.GenerateToken(_configuration, userId.ToString());
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync("/api/UserProfile");

        response.EnsureSuccessStatusCode();
        var profile = await response.Content.ReadFromJsonAsync<UserProfileDto>();
        Assert.NotNull(profile);
        Assert.Equal("testuser", profile.Username);
        Assert.Equal("This is a test bio", profile.Bio);
    }

    [Fact]
    public async Task UpdateProfile_ShouldUpdate_UserProfile() {
        var userId = Guid.NewGuid();
        using var arrangeScope = _factory.Services.CreateScope();
        var db = arrangeScope.ServiceProvider.GetRequiredService<ChatAppDbContext>();


        db.Users.Add(new User
        {
            Id = userId,
            Username = "old_user",
            Email = "old@example.com",
            PasswordHash = "old_password",
            Bio = "This is a test bio",
            ProfilePictureUrl = "test.jpg"
        });

        await db.SaveChangesAsync();

        var updateDto = new UpdateUserProfileDto
        {
            Bio = "new bio",
            ProfilePictureUrl = "new.jpg"
        };

        var token =  JwtTokenHelper.GenerateToken(_configuration, userId.ToString());
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PutAsJsonAsync("/api/UserProfile", updateDto);

        // Assert
        response.EnsureSuccessStatusCode();

        using var assertScope = _factory.Services.CreateScope();
        var updatedDb = assertScope.ServiceProvider.GetRequiredService<ChatAppDbContext>();

        var updatedUser = await updatedDb.Users.FindAsync(userId);
        Assert.Equal("new bio", updatedUser?.Bio);
        Assert.Equal("new.jpg", updatedUser?.ProfilePictureUrl);
        Assert.Equal("old_user", updatedUser?.Username);

    }
}