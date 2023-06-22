using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Tests.IntegrationTests;

public class HomePageTest : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;

    public HomePageTest(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact(DisplayName = "GET - check that the homepage loads")]
    public async Task TestHomePageLoads()
    {
        var response = await _client.GetAsync("/");

        response.EnsureSuccessStatusCode();
        
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }
}