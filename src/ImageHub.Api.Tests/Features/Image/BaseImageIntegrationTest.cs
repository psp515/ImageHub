using ImageHub.Api.Tests.Shared.Builders;
using ImageHub.Api.Tests.Shared.Models;

namespace ImageHub.Api.Tests.Features.Image;

public class BaseImageIntegrationTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    protected async Task<MultipartFormDataContent> GetPngWithoutAntiforgery()
        => await new ImageFormDataBuilder(_client)
        .WithoutAntiforgery()
        .Build();

    protected async Task<MultipartFormDataContent> GetJpegWithoutAntiforgery() 
        => await new ImageFormDataBuilder(_client)
        .WithoutAntiforgery()
        .WithType(ImagesTypes.Jpeg)
        .Build();

    protected async Task<MultipartFormDataContent> GetPngWithTooLongName()
        => await new ImageFormDataBuilder(_client)
        .WithTooLongName()
        .Build();

    protected async Task<MultipartFormDataContent> GetPng()
        => await new ImageFormDataBuilder(_client)
        .Build();

    protected async Task<MultipartFormDataContent> GetJpeg() 
        => await new ImageFormDataBuilder(_client)
        .WithType(ImagesTypes.Jpeg)
        .Build();

    protected async Task<MultipartFormDataContent> GetGif() 
        => await new ImageFormDataBuilder(_client)
        .WithType(ImagesTypes.Gif)
        .Build();
}
