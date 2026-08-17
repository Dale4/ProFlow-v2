using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ProFlow.Api.Tests;

public class RootEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RootEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetRoot_ReturnsHelloWorld()
    {
        using var response = await _client.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Hello World", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetLogs_ReturnsFirstTwoLogEntries()
    {
        using var seed = await _client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, seed.StatusCode);

        using var response = await _client.GetAsync("/logs");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var logs = await response.Content.ReadFromJsonAsync<string[]>();
        Assert.NotNull(logs);
        Assert.Equal(2, logs.Length);
        Assert.All(logs, line => Assert.False(string.IsNullOrWhiteSpace(line)));
    }
}
