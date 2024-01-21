using ImageHub.Api.Tests.Features.Image.Models;
using System.Net;
using System.Reflection;

namespace ImageHub.Api.Tests.Features.Image;

public class ImageIntegrationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{

    private async Task<MultipartFormDataContent> BaseMultipart()
    {
        var name = $"Test {Guid.NewGuid()}";
        var description = "Test Image Description.";

        var response = await _client.GetAsync("/api/security/antiforgery/token");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await TestsCommon.DeserializeResponse<AntiforgeryTokenResponse>(response);
        Assert.NotNull(body);
        Assert.NotNull(body.Token);

        var formContent = new MultipartFormDataContent
        {
            { new StringContent(description), "description" },
            { new StringContent(name), "name" },
            { new StringContent(body.Token), "csrfToken" }
        };

        return formContent;
    }

    private async Task<MultipartFormDataContent> GetPng()
    {
        var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var filePath = Path.Combine(dirName, "TestData/Images/png.png");
        var fileName = Path.GetFileName(filePath);
        var stream = new StreamContent(File.OpenRead(filePath));
        stream.Headers.ContentType = new("image/png");

        var formContent = await BaseMultipart();
        formContent.Add(stream, "image", fileName);

        return formContent;
    }

    private async Task<MultipartFormDataContent> GetJpeg()
    {
        var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var filePath = Path.Combine(dirName, "TestData/Images/jpeg.jpg");
        var fileName = Path.GetFileName(filePath);
        var stream = new StreamContent(File.OpenRead(filePath));
        stream.Headers.ContentType = new("image/jpeg");

        var formContent = await BaseMultipart();
        formContent.Add(stream, "image", fileName);

        return formContent;
    }

    private async Task<MultipartFormDataContent> GetGif()
    {
        var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var filePath = Path.Combine(dirName, "TestData/Images/gif.gif");
        var fileName = Path.GetFileName(filePath);
        var stream = new StreamContent(File.OpenRead(filePath));
        stream.Headers.ContentType = new("image/gif");

        var formContent = await BaseMultipart();
        formContent.Add(stream, "image", fileName);

        return formContent;
    }

    [Fact]
    public async Task AbleToAddPng()
    {
        //Arrange
        var formContent = await GetPng();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task AbleToAddJpeg()
    {
        //Arrange
        var formContent = await GetJpeg();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task UnableToAddGif()
    {
        //Arrange
        var formContent = await GetGif();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
