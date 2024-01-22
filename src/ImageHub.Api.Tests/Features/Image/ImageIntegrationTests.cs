using ImageHub.Api.Tests.Features.Image.Models;
using System.Net;
using System.Reflection;

namespace ImageHub.Api.Tests.Features.Image;

public class ImageIntegrationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private async Task<MultipartFormDataContent> BaseMultipart(string fileRelativePath, string fileType, bool antiforgery)
    {
        var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var filePath = Path.Combine(dirName, fileRelativePath);
        var stream = new StreamContent(File.OpenRead(filePath));
        stream.Headers.ContentType = new(fileType);

        var formContent = new MultipartFormDataContent
        {
            { new StringContent("Test Image Description."), "description" },
            { new StringContent($"Test {Guid.NewGuid()}"), "name" },
            { stream, "image", Path.GetFileName(filePath) }
        };

        if (antiforgery)
        {
            var response = await _client.GetAsync("/api/security/antiforgery/token");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var body = await TestsCommon.DeserializeResponse<AntiforgeryTokenResponse>(response);
            Assert.NotNull(body);
            Assert.NotNull(body.Token);

            formContent.Add(new StringContent(body.Token), "csrfToken");
        }

        return formContent;
    }

    private async Task<MultipartFormDataContent> GetPng(bool antiforgery = true) 
        => await BaseMultipart("TestData/Images/png.png", "image/png", antiforgery);

    private async Task<MultipartFormDataContent> GetJpeg(bool antiforgery = true) 
        => await BaseMultipart("TestData/Images/jpeg.jpg", "image/jpeg", antiforgery);

    private async Task<MultipartFormDataContent> GetGif(bool antiforgery = true)
        => await BaseMultipart("TestData/Images/gif.gif", "image/gif", antiforgery);

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
        var formContent = await GetPng(false);

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
