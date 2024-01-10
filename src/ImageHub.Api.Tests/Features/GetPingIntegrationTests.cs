using System.Net;

namespace ImageHub.Api.Tests.Features;

public class GetPingIntegrationTests : IClassFixture<ApiFactory>
{
    readonly HttpClient _client;

    public GetPingIntegrationTests(ApiFactory application)
    {
        _client = application.CreateClient();
    }

    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        var response = await _client.GetAsync("/api/ping/psp515");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
