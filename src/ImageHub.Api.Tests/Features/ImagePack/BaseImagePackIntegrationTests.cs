using ImageHub.Api.Contracts.ImagePacks.AddImagePack;

namespace ImageHub.Api.Tests.Features.ImagePack;

public class BaseImagePackIntegrationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{

    public async Task<HttpResponseMessage> AddImagePackWithData(string? name = null, string? description = null)
    {
        var request = new AddImagePackRequest()
        {
            Name = name ?? $"Test {Guid.NewGuid()}",
            Description = description ?? "Test Image Pack Description."
        };

        var content = TestsCommon.Serialize(request);

        var response = await _client.PostAsync("/api/imagepacks", content);

        return response ?? new HttpResponseMessage();
    }
}
