﻿using ImageHub.Api.Tests.Features.Image.Models;
using ImageHub.Api.Tests.Shared.Builders;
using ImageHub.Api.Tests.Shared.Responses;
using System.Net;

namespace ImageHub.Api.Tests.Features.Image;

public class GetImageIntegrationTests(IntegrationTestWebAppFactory factory) 
    : BaseImageIntegrationTest(factory)
{
    [Fact]
    public async Task GetImage()
    {
        //Arrange
        var name = Guid.NewGuid()
            .ToString();
        var formContent = await new ImageFormDataBuilder(_client)
            .WithName($"{name}")
            .Build();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);
        var result = await TestsCommon.Deserialize<IdResponse>(response);
        var getImageResponse = await _client.GetAsync($"/api/images/{result?.Id}");

        var resultObject = await TestsCommon.Deserialize<ImageDto>(getImageResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getImageResponse.StatusCode);

        Assert.Equal(name, resultObject!.Name);
    }

    [Fact]
    public async Task NotFoundImage()
    {
        //Arrange
        var formContent = await GetPng();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);
        var getImageResponse = await _client.GetAsync($"/api/images/{Guid.NewGuid()}");

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getImageResponse.StatusCode);
        var result = await TestsCommon.Deserialize<ErrorResponse>(getImageResponse);
        Assert.Equal(1, result.Errors.Count);
    }
}
