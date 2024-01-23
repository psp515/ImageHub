using ImageHub.Api.Tests.Shared.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.VisualBasic.FileIO;
using System.Net;
using System.Reflection;

namespace ImageHub.Api.Tests.Shared.Builders;

public class ImageFormDataBuilder
{
    private string Name { get; set; } = $"Test {Guid.NewGuid()}";
    private string Description { get; set; } = "Test Image Description.";
    private ImagesTypes Type { get; set; } = ImagesTypes.Png;
    private bool Antiforgery { get; set; } = true;

    private readonly HttpClient _client;

    public ImageFormDataBuilder(HttpClient client)
    {
        _client = client;
    }

    public async Task<MultipartFormDataContent> Build()
    {

        var (fileType, fileRelativePath) = FileDataFromType(Type);

        var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var filePath = Path.Combine(dirName, fileRelativePath);
        var stream = new StreamContent(File.OpenRead(filePath));
        stream.Headers.ContentType = new(fileType);

        var formContent = new MultipartFormDataContent
        {
            { new StringContent(Description), "description" },
            { new StringContent(Name), "name" },
            { stream, "image", Path.GetFileName(filePath) }
        };

        if (Antiforgery)
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

    public ImageFormDataBuilder WithName(string name)
    {
        this.Name = name;
        return this;
    }

    public ImageFormDataBuilder WithTooLongName()
    {
        Name = $"{Guid.NewGuid()}{Guid.NewGuid()}";
        return this;
    }

    public ImageFormDataBuilder WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public ImageFormDataBuilder WithTooLongDescription()
    {
        Name = new string('*', 600);
        return this;
    }

    public ImageFormDataBuilder WithType(ImagesTypes type)
    {
        Type = type;
        return this;
    }

    public ImageFormDataBuilder WithoutAntiforgery()
    {
        Antiforgery = false;
        return this;
    }

    private static (string, string) FileDataFromType(ImagesTypes type) => type switch
    {
        ImagesTypes.Png => ("image/png", "TestData/Images/png.png"),
        ImagesTypes.Jpeg => ("image/jpeg", "TestData/Images/jpeg.jpg"),
        ImagesTypes.Gif => ("image/gif", "TestData/Images/gif.gif"),
        _ => throw new NotImplementedException()
    };
}
