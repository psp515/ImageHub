using ImageHub.Api.Tests.Shared.Responses;
using System.Net;

namespace ImageHub.Api.Tests.Features.ImagePack;

public class DeleteImagePackIntegrationTests(IntegrationTestWebAppFactory factory) : BaseImagePackIntegrationTests(factory)
{
    [Fact]
    public async Task DeleteImagePack()
    {
        //Arrange

        //Act
        var response = await AddImagePackWithData();
        var id = await TestsCommon.Deserialize<IdResponse>(response);
        var deleteResponse = await _client.DeleteAsync($"/api/imagepacks/{id!.Id}");

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteUnexitingImagePackFails()
    {
        //Arrange
        var packId = Guid.NewGuid();

        //Act
        var response = await AddImagePackWithData();
        var deleteResponse = await _client.DeleteAsync($"/api/imagepacks/{packId}");

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }
}
