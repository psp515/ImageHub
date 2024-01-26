using ImageHub.Api.Tests.Features.Image.Models;
using ImageHub.Api.Tests.Shared.Responses;
using System.Net;

namespace ImageHub.Api.Tests.Features.Image;

public class UpdateImageIntegrationTests(IntegrationTestWebAppFactory factory) : BaseImageIntegrationTest(factory)
{
    [Fact]
    public async Task UpdateImage()
    {
        //Arrange
        var updateRequest = new UpdateImageRequest()
        {
            Description = "Updated Test Image Description."
        };
        var updateContent = TestsCommon.Serialize(updateRequest);
        var formContent = await GetPng();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);
        var idObject = await TestsCommon.Deserialize<IdResponse>(response);
        var updateResponse = await _client.PatchAsync($"/api/images/{idObject.Id}", updateContent);
        var getResponse = await _client.GetAsync($"/api/images/{idObject!.Id}");
        var image = await TestsCommon.Deserialize<ImageDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(updateRequest.Description, image.Description);
    }

    [Fact]
    public async Task UpdateUnexistingImageFails()
    {
        //Arrange

        var updateRequest = new UpdateImageRequest()
        {
            Description = "Updated Test Image Pack Description."
        };
        var updateContent = TestsCommon.Serialize(updateRequest);
        var formContent = await GetPng();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);
        var idObject = await TestsCommon.Deserialize<IdResponse>(response);
        var updateResponse = await _client.PatchAsync($"/api/images/{Guid.NewGuid()}", updateContent);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
    }
}
