using ImageHub.Api.Contracts.ImagePacks.AddImagePack;
using ImageHub.Api.Entities;
using ImageHub.Api.Features.Images.AddImage;
using ImageHub.Api.Features.Thumbnails;
using ImageHub.Api.Tests.Features.Image.Models;
using ImageHub.Api.Tests.Features.Thumbnails.Models;
using ImageHub.Api.Tests.Shared.Responses;
using MassTransit.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
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
    public async Task AddPngNotifiesIfThumbanilIsProcessed()
    {
        //Arrange
        var formContent = await GetPng();
        var connection = new HubConnectionBuilder()
            .WithUrl($"ws://localhost/thumbnailsHub",
                     o => o.HttpMessageHandlerFactory = _ => _handler)
            .Build();

        bool received = false;
        ProcessingStatus processingStatus = ProcessingStatus.NotStarted;

        connection.On<string, ProcessingStatus>("ThumbnailProcessed", (id, status) =>
        {
            processingStatus = status;
            received = true;
        });

        try
        {
            await connection.StartAsync();
        }
        catch (Exception e)
        {
            processingStatus = ProcessingStatus.NotStarted;
        }

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        for(int i = 0; i < 100 && !received; i++)
        {
            await Task.Delay(100);
        }

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(ProcessingStatus.Success, processingStatus);
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
    public async Task AddPngToImagePack()
    {
        //Arrange
        var formContent = await GetPng();
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };
        var content = TestsCommon.Serialize(request);

        //Act
        var packResponse = await _client.PostAsync("/api/imagepacks", content);
        var packId = await TestsCommon.Deserialize<IdResponse>(packResponse);
        var response = await _client.PostAsync($"/api/images?pack={packId.Id}", formContent);
        var ids = await TestsCommon.Deserialize<AddImageResponse>(response);
        var location = response.Headers.Location;
        var getResponse = await _client.GetAsync(location);
        var image = await TestsCommon.Deserialize<ImageDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, packResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(packId.Id.ToString(), image.PackId);
        Assert.Equal($"api/images/{ids.Id}", location!.ToString());
    }

    [Fact]
    public async Task AddPngToImagePackIgnoreInvalidGuid()
    {
        //Arrange
        var formContent = await GetPng();
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };
        var content = TestsCommon.Serialize(request);

        //Act
        var packResponse = await _client.PostAsync("/api/imagepacks", content);
        var response = await _client.PostAsync($"/api/images?pack={345345}", formContent);
        var id = await TestsCommon.Deserialize<AddImageResponse>(packResponse);
        var getResponse = await _client.GetAsync($"/api/images/{id.Id}");
        var image = await TestsCommon.Deserialize<ImageDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, packResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Null(image.PackId);
    }

    [Fact]
    public async Task AddPngCreatesThumbnailAndThumbnailIsProcessedSuccessfully()
    {
        //Arrange
        var formContent = await GetPng();
        var harness = factory.Services.GetRequiredService<ITestHarness>();

        //Act
        var response = await _client.PostAsync("/api/images", formContent);

        await harness.Sent.Any<AddImageEvent>();
        await harness.Consumed.Any<AddImageEventConsumer>();

        await Task.Delay(1000);

        var idObject = await TestsCommon.Deserialize<AddImageResponse>(response);
        var getResponse = await _client.GetAsync($"/api/thumbnails/{idObject.ThumbnailId}");
        var thumbnail = await TestsCommon.Deserialize<ThumbnailDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(idObject.ThumbnailId, thumbnail!.Id);
        Assert.Equal(ThumbnailExtensions.ThumbnailExtension, thumbnail.FileExtension);
        Assert.Equal(ThumbnailExtensions.BaseEncoding, thumbnail.Encoding);
        Assert.Equal(ProcessingStatus.Success, thumbnail.ProcessingStatus);
        Assert.False(string.IsNullOrWhiteSpace(thumbnail.EncodedImage));
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
