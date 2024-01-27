using ImageHub.Api.Tests.Shared.Builders;
using ImageHub.Api.Tests.Shared.Models;

namespace ImageHub.Api.Tests.Features.Thumbnails;

public class BaseThumbnailIntegrationTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    protected async Task<MultipartFormDataContent> GetPng()
        => await new ImageFormDataBuilder(_client)
        .Build();

    protected async Task<MultipartFormDataContent> GetJpeg() 
        => await new ImageFormDataBuilder(_client)
        .WithType(ImagesTypes.Jpeg)
        .Build();
}
