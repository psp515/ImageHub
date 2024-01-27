using ImageHub.Api.Tests.Features.Thumbnails.Models;
using System.Net;

namespace ImageHub.Api.Tests.Features.Thumbnails;

public class GetThumbnailsIntegrationTests(IntegrationTestWebAppFactory factory) : BaseThumbnailIntegrationTest(factory)
{
    [Fact]
    public async Task GetThumbnails()
    {
        //Arrange
        var image1 = await GetPng();
        var image2 = await GetPng();

        //Act
        var response1 = await _client.PostAsync("/api/images", image1);
        var response2 = await _client.PostAsync("/api/images", image2);

        var getResponse = await _client.GetAsync($"/api/thumbnails");

        var thumbnails = await TestsCommon.Deserialize<ThumbnailsDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.True(thumbnails!.Thumbnails.Count() >= 2);
    }

    [Fact]
    public async Task GetSecondPageOfThumbnailsFails()
    {
        //Arrange

        //Act   

        var getResponse = await _client.GetAsync($"/api/imagepacks?page={2}");

        //Assert

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task GetThumbnailsWithPagination()
    {
        //Arrange
        var image1 = await GetPng();
        var image2 = await GetPng();

        //Act
        var response1 = await _client.PostAsync("/api/images", image1);
        var response2 = await _client.PostAsync("/api/images", image2);

        var getResponse = await _client.GetAsync($"/api/thumbnails?page=1&size=1");
        var get2Response = await _client.GetAsync($"/api/thumbnails?page=2&size=1");

        //Assert
        Assert.Equal(HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var thumbnails = await TestsCommon.Deserialize<ThumbnailsDto>(getResponse);
        Assert.True(thumbnails!.Thumbnails.Count() == 1);
        Assert.Equal(HttpStatusCode.OK, get2Response.StatusCode);
        var thumbnails2 = await TestsCommon.Deserialize<ThumbnailsDto>(get2Response);
        Assert.True(thumbnails2!.Thumbnails.Count() == 1);
    }
}
