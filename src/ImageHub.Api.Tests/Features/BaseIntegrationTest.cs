namespace ImageHub.Api.Tests.Features;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly HttpClient _client;
    protected readonly HttpMessageHandler _handler;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _client = factory.CreateClient();
        _handler = factory.Server.CreateHandler();
    }
}
