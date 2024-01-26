using ImageHub.Api.Tests.Features.Image.Models;
using ImageHub.Api.Tests.Shared.Builders;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ImageHub.Api.Tests.Features.Image;

public class GetImagesIntegrationTests(IntegrationTestWebAppFactory factory) :
    BaseImageIntegrationTest(factory)
{
    [Fact]
    public async Task GetImages()
    {
        //Arrange
        var name = Guid.NewGuid();
        var formContent1 = await GetPng();
        var formContent2 = await new ImageFormDataBuilder(_client)
            .WithName($"{name}")
            .Build();

        //Act
        var response1 = await _client.PostAsync("/api/images", formContent1);
        var response2 = await _client.PostAsync("/api/images", formContent2);

        var getResponse = await _client.GetAsync($"/api/images");

        var images = await TestsCommon.Deserialize<ImagesDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.True(images!.Images.Count >= 2);

        Assert.Contains(images.Images, x => x.Name == $"{name}");
    }

    [Fact]
    public async Task GetSecondPageOfImagesNotFound()
    {
        //Arrange

        //Act   
        var getResponse = await _client.GetAsync($"/api/images?page={2}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task GetSecondPageOfImages()
    {
        //Arrange
        var name = Guid.NewGuid()
            .ToString();
        var formContent1 = await GetPng();
        var formContent2 = await new ImageFormDataBuilder(_client)
            .WithName($"{name}")
            .Build();

        //Act
        var response1 = await _client.PostAsync("/api/images", formContent1);
        var response2 = await _client.PostAsync("/api/images", formContent2);

        var getResponse1 = await _client.GetAsync($"/api/images?page=1&size=1");
        var getResponse2 = await _client.GetAsync($"/api/images?page=2&size=1");

        var images1 = await TestsCommon.Deserialize<ImagesDto>(getResponse1);
        var images2 = await TestsCommon.Deserialize<ImagesDto>(getResponse2);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);

        Assert.Equal(HttpStatusCode.OK, getResponse1.StatusCode);
        Assert.True(images1!.Images.Count == 1);

        Assert.Equal(HttpStatusCode.OK, getResponse2.StatusCode);
        Assert.True(images2!.Images.Count == 1);

        Assert.True(images1.Images[0].Name == name || images2.Images[0].Name == name);
    }

}
