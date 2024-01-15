using System.Net;

namespace ImageHub.Api.Tests.Features.Ping;

public class GetPingIntegrationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CandPingEndpoint()
    {
        //Arrange

        //Act
        var response = await _client.GetAsync("/api/ping/psp515");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
