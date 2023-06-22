using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1._0.Identity;
using Xunit.Abstractions;

namespace Tests.IntegrationTests.API.v1._0;

public class IdentityTest : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly string _registerURL;

    public IdentityTest(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _registerURL = "/api/v1.0/Identity/Account/Register";
    }

    [Fact(DisplayName = "POST - register new user failure, only admin")]
    public async Task TestRegisterNewUserFailed()
    {
        var registerData = new
        {
            Email = "test@app.com",
            Password = "a",
            FirstName = "Test",
            LastName = "App"
        };
        var data = JsonContent.Create(registerData);

        var response = await _client.PostAsync(_registerURL, data);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "POST - login user")]
    public async Task TestLoginUser()
    {
        
    }

    [Fact(DisplayName = "POST - login failed")]
    public async Task TestLoginFailed()
    {
        
    }

    [Fact(DisplayName = "POST - register new user")]
    public async Task TestRegisterNewUser()
    {
        var registerData = new
        {
            Email = "test@app.com",
            Password = "Foo.bar1",
            FirstName = "Test",
            LastName = "App"
        };
        var data = JsonContent.Create(registerData);

        var response = await _client.PostAsync(_registerURL, data);

        Assert.True(response.IsSuccessStatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var JWTResponse = System.Text.Json.JsonSerializer.Deserialize<RefreshTokenModel>(responseContent);
        
        Assert.NotNull(JWTResponse);
    }

    [Fact(DisplayName = "POST - JWT expired")]
    public async Task TestJWTExpired()
    {
        
    }

    [Fact(DisplayName = "POST - JWT renewal")]
    public async Task TestJWTRenewal()
    {
        
    }

    [Fact(DisplayName = "POST - JWT logout")]
    public async Task JWTLogout()
    {
        
    }
}