using ImageHub.Api.Tests.Features.Image.Models;
using System.Net;
using System.Reflection;

namespace ImageHub.Api.Tests.Features.Image;

public class BaseImageIntegrationTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
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

    protected async Task<MultipartFormDataContent> GetPngWithoutAntiforgery()
        => await BaseMultipart("TestData/Images/png.png", "image/png", false);
    protected async Task<MultipartFormDataContent> GetPngWithTooLongName()
        => await BaseMultipart("TestData/Images/png.png", "image/png", true);

    protected async Task<MultipartFormDataContent> GetPng()
        => await BaseMultipart("TestData/Images/png.png", "image/png", true);

    protected async Task<MultipartFormDataContent> GetJpeg(bool antiforgery = true)
        => await BaseMultipart("TestData/Images/jpeg.jpg", "image/jpeg", antiforgery);

    protected async Task<MultipartFormDataContent> GetGif(bool antiforgery = true)
        => await BaseMultipart("TestData/Images/gif.gif", "image/gif", antiforgery);
}
