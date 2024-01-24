using ImageHub.Api.Tests.Shared.Responses;
using System.Net;

namespace ImageHub.Api.Tests.Features.Image;

public class DeleteImageIntegrationTests(IntegrationTestWebAppFactory factory) 
    : BaseImageIntegrationTest(factory)
{
    [Fact]
    public async Task DeleteImage()
    {
        //Arrange
        var formContent = await GetPng();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);
        var result = await TestsCommon.Deserialize<IdResponse>(response);
        var getImageResponse = await _client.GetAsync($"/api/images/{result?.Id}");
        var deleteImageResponse = await _client.DeleteAsync($"/api/images/{result?.Id}");

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getImageResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, deleteImageResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteNotFoundImage()
    {
        //Arrange
        var formContent = await GetPng();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);
        var getImageResponse = await _client.DeleteAsync($"/api/images/{Guid.NewGuid()}");

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getImageResponse.StatusCode);
        var result = await TestsCommon.Deserialize<ErrorResponse>(getImageResponse);
        Assert.Equal(1, result.Errors.Count);
    }
}
