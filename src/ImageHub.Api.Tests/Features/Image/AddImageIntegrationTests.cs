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
    public async Task FailToAddJpgWithoutAntiforgeryToken()
    {
        //Arrange
        var formContent = await GetJpeg(false);

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
