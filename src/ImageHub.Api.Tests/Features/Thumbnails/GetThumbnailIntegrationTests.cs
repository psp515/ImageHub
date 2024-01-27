using ImageHub.Api.Tests.Features.Thumbnails.Models;
using ImageHub.Api.Tests.Shared.Responses;
using System.Net;

namespace ImageHub.Api.Tests.Features.Thumbnails;

public class GetThumbnailIntegrationTests(IntegrationTestWebAppFactory factory) : BaseThumbnailIntegrationTest(factory)
{
    [Fact]
    public async Task GetThumbnail()
    {
        //Arrange
        var image = await GetPng();

        //Act
        var addResponse = await _client.PostAsync("/api/images", image);
        var idObject = await TestsCommon.Deserialize<AddImageResponse>(addResponse);
        var response = await _client.GetAsync($"/api/thumbnails/{idObject.ThumbnailId}");

        //Assert
        Assert.Equal(HttpStatusCode.Created, addResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var thumbnail = await TestsCommon.Deserialize<ThumbnailDto>(response);
        Assert.Equal(idObject.ThumbnailId, thumbnail!.Id);
        Assert.Equal(idObject.Id, thumbnail.ImageId);
    }

    [Fact]
    public async Task GetUnexistingThumbnailFails()
    {
        //Arrange
        var image = await GetPng();

        //Act
        var addResponse = await _client.PostAsync("/api/images", image);
        var response = await _client.GetAsync($"/api/thumbnail/{Guid.NewGuid()}");

        //Assert
        Assert.Equal(HttpStatusCode.Created, addResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
