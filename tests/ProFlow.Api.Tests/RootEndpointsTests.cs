using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using ProFlow.Api;

namespace ProFlow.Api.Tests;

public class RootEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RootEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetRoot_ReturnsHelloWorld()
    {
        using var client = CreateClient(logsEndpointEnabled: true);
        using var response = await client.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Hello World", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetLogs_WhenFeatureEnabled_ReturnsFirstTwoLogEntries()
    {
        using var client = CreateClient(logsEndpointEnabled: true);

        using var seed = await client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, seed.StatusCode);

        using var response = await client.GetAsync("/logs");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var logs = await response.Content.ReadFromJsonAsync<string[]>();
        Assert.NotNull(logs);
        Assert.Equal(2, logs.Length);
        Assert.All(logs, line => Assert.False(string.IsNullOrWhiteSpace(line)));
    }

    [Fact]
    public async Task GetLogs_WhenFeatureDisabled_ReturnsNotFound()
    {
        using var client = CreateClient(logsEndpointEnabled: false);

        using var response = await client.GetAsync("/logs");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private HttpClient CreateClient(bool logsEndpointEnabled) =>
        _factory.WithWebHostBuilder(builder =>
            builder.ConfigureAppConfiguration((_, config) =>
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    [$"FeatureManagement:{FeatureFlags.LogsEndpoint}"] = logsEndpointEnabled.ToString()
                }))).CreateClient();
}
