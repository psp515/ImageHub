using ImageHub.Api.Tests.Shared.Responses;
using System.Net;

namespace ImageHub.Api.Tests.Features.Image;

public class AddImageIntegrationTests(IntegrationTestWebAppFactory factory) : BaseImageIntegrationTest(factory)
{
    [Fact]
    public async Task AddPng()
    {
        //Arrange
        var formContent = await GetPng();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task AddJpeg()
    {
        //Arrange
        var formContent = await GetJpeg();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task FailToAddGif()
    {
        //Arrange
        var formContent = await GetGif();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task FailToAddPngWithoutAntiforgeryToken()
    {
        //Arrange
        var formContent = await GetPngWithoutAntiforgery();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task FailToAddTwoPngWithSameName()
    {
        //Arrange
        var formContent = await GetPng();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);
        var copyResponse = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, copyResponse.StatusCode);
    }

    [Fact]
    public async Task FailToAddJpgWithoutAntiforgeryToken()
    {
        //Arrange
        var formContent = await GetJpegWithoutAntiforgery();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task FailToAddPngWithTooLongName()
    {
        //Arrange
        var formContent = await GetPngWithTooLongName();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await TestsCommon.Deserialize<ErrorResponse>(response);
        Assert.Equal(1, result.Errors.Count);
    }

    [Fact]
    public async Task FailToAddPngWithTooLongDescription()
    {
        //Arrange
        var formContent = await GetPngWithTooLongDescription();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await TestsCommon.Deserialize<ErrorResponse>(response);
        Assert.Equal(1, result.Errors.Count);
    }

}
