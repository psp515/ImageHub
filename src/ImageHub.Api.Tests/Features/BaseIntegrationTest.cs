namespace ImageHub.Api.Tests.Features;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly HttpClient _client;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }
}
