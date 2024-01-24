using ImageHub.Api.Contracts.ImagePacks.UpdateImagePack;
using ImageHub.Api.Tests.Features.ImagePack.Models;
using ImageHub.Api.Tests.Shared.Responses;
using System.Net;

namespace ImageHub.Api.Tests.Features.ImagePack;

public class UpdateImagePackIntegrationTests(IntegrationTestWebAppFactory factory) : BaseImagePackIntegrationTests(factory)
{
    [Fact]
    public async Task UpdateImagePack()
    {
        //Arrange
        var name = $"Test {Guid.NewGuid()}";

        var updateRequest = new UpdateImagePackRequest()
        {
            Description = "Updated Test Image Pack Description."
        };
        var updateContent = TestsCommon.Serialize(updateRequest);

        //Act
        var response = await AddImagePackWithData(name);
        var idObject = await TestsCommon.Deserialize<IdResponse>(response);
        var updateResponse = await _client.PatchAsync($"/api/imagepacks/{idObject.Id}", updateContent);
        var getResponse = await _client.GetAsync($"/api/imagepacks/{idObject!.Id}");
        var pack = await TestsCommon.Deserialize<ImagePackDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(name, pack.Name);
        Assert.Equal(updateRequest.Description, pack.Description);
    }

    [Fact]
    public async Task UpdateUnexistingImagePackFails()
    {
        //Arrange
        var updateRequest = new UpdateImagePackRequest()
        {
            Description = "Updated Test Image Pack Description."
        };
        var updateContent = TestsCommon.Serialize(updateRequest);

        //Act
        var response = await AddImagePackWithData();

        var updateResponse = await _client.PatchAsync($"/api/imagepacks/{Guid.NewGuid()}", updateContent);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
    }
}
