using ImageHub.Api.Tests.Shared.Responses;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace ImageHub.Api.Tests.Features.Image;

public class GetImageFileIntegrationTests(IntegrationTestWebAppFactory factory) :
    BaseImageIntegrationTest(factory)
{

    [Fact]
    public async Task ReturningPng()
    {
        // Arrange
        var image = await GetPng();
        var imageContent = image.First(c => c.Headers.ContentDisposition!.Name == "image");
        using var imageStream = await imageContent.ReadAsStreamAsync();

        // Act
        var addResponse = await _client.PostAsync("/api/images", image);
        var idObject = await TestsCommon.Deserialize<IdResponse>(addResponse);
        var response = await _client.GetAsync($"/api/images/{idObject.Id}/file");
        using var responseStream = await response.Content.ReadAsStreamAsync();

        // Assert

        Assert.Equal(HttpStatusCode.Created, addResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("image/png", response.Content.Headers.ContentType!.MediaType);
        Assert.Equal(imageStream.Length, response.Content.Headers.ContentLength);
    }

    [Fact]
    public async Task ReturningJpeg()
    {
        // Arrange
        var image = await GetJpeg();
        var imageContent = image.First(c => c.Headers.ContentDisposition!.Name == "image");
        using var imageStream = await imageContent.ReadAsStreamAsync();

        // Act
        var addResponse = await _client.PostAsync("/api/images", image);
        var idObject = await TestsCommon.Deserialize<IdResponse>(addResponse);
        var response = await _client.GetAsync($"/api/images/{idObject.Id}/file");
        using var responseStream = await response.Content.ReadAsStreamAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Created, addResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("image/jpeg", response.Content.Headers.ContentType!.MediaType);
        Assert.Equal(imageStream.Length, response.Content.Headers.ContentLength);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenImageDoesNotExist()
    {
        // Arrange
        var imageId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/images/{imageId}/file");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
