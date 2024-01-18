using ImageHub.Api.Contracts.Image.AddImage;
using System.Net;
using System.Reflection;

namespace ImageHub.Api.Tests.Features.Image;

public class ImageIntegrationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private (StreamContent, string) GetPng()
    {
        var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var filePath = Path.Combine(dirName, "TestData/Images/png.png");
        var fileName = Path.GetFileName(filePath);

        var stream = new StreamContent(File.OpenRead(filePath));
        stream.Headers.ContentType = new("image/png");

        return (stream, fileName);
    }

    private (StreamContent, string) GetJpeg()
    {
        var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var filePath = Path.Combine(dirName, "TestData/Images/jpeg.jpg");
        var fileName = Path.GetFileName(filePath);

        var stream = new StreamContent(File.OpenRead(filePath));
        stream.Headers.ContentType = new("image/jpeg");

        return (stream, fileName);
    }

    private (StreamContent, string) GetGif()
    {
        var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var filePath = Path.Combine(dirName, "TestData/Images/gif.gif");
        var fileName = Path.GetFileName(filePath);

        var stream = new StreamContent(File.OpenRead(filePath));
        stream.Headers.ContentType = new("image/gif");

        return (stream, fileName);
    }

    [Fact]
    public async Task AbleToAddPng()
    {
        //Arrange
        (var stream, var fileName) = GetPng();
        var name = $"Test {Guid.NewGuid()}";
        var description = "Test Image Description.";

        var formContent = new MultipartFormDataContent();
        formContent.Add(stream, "image", fileName);
        formContent.Add(new StringContent(description), "description");
        formContent.Add(new StringContent(name), "name");

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await TestsCommon.DeserializeResponse<AddImageResponse>(response);
        Assert.NotNull(body);
    }

    [Fact]
    public async Task AbleToAddJpeg()
    {
        //Arrange
        (var jpegStream, var fileName) = GetJpeg();
        var name = $"Test {Guid.NewGuid()}";
        var description = "Test Image Description.";

        var formContent = new MultipartFormDataContent();
        formContent.Add(jpegStream, "image", fileName);
        formContent.Add(new StringContent(description), "description");
        formContent.Add(new StringContent(name), "name");

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await TestsCommon.DeserializeResponse<AddImageResponse>(response);
        Assert.NotNull(body);
    }

    [Fact]
    public async Task UnableToAddGif()
    {
        //Arrange
        (var gifStream, var fileName) = GetGif();
        var name = $"Test {Guid.NewGuid()}";
        var description = "Test Image Description.";

        var formContent = new MultipartFormDataContent();
        formContent.Add(gifStream, "image", fileName);
        formContent.Add(new StringContent(description), "description");
        formContent.Add(new StringContent(name), "name");

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


}
