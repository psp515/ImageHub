using System.Net;

namespace ImageHub.Api.Tests.Features.Ping;

public class GetPingIntegrationTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public GetPingIntegrationTests(ApiFactory application)
    {
        _client = application.CreateClient();
    }

    [Fact]
    public async Task CandPingEndpoint()
    {
        var response = await _client.GetAsync("/api/ping/psp515");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
