using ImageHub.Api.Contracts.ImagePacks.AddImagePack;
using ImageHub.Api.Contracts.ImagePacks.UpdateImagePack;
using ImageHub.Api.Tests.Models;
using System.Net;

namespace ImageHub.Api.Tests.Features.ImagePack;

public class ImagePackIntegrationTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public ImagePackIntegrationTests(ApiFactory application)
    {
        _client = application.CreateClient();
    }



    [Fact]
    public async Task AddImagePack()
    {
        //Given
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var content = TestsCommon.CreateHttpContent(request);

        //When
        var response = await _client.PostAsync("/api/imagepacks", content);

        //Then
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task FailAddImagePackWithSameName()
    {
        //Given
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var content = TestsCommon.CreateHttpContent(request);

        //When
        var response = await _client.PostAsync("/api/imagepacks", content);
        var copyResponse = await _client.PostAsync("/api/imagepacks", content);

        //Then
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, copyResponse.StatusCode);
    }

    [Fact]
    public async Task GetImagePack()
    {
        //Given
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var content = TestsCommon.CreateHttpContent(request);

        //When
        var response = await _client.PostAsync("/api/imagepacks", content);
        var idObject = await TestsCommon.DeserializeResponse<IdResponse>(response);
        var getResponse = await _client.GetAsync($"/api/imagepacks/{idObject!.Id}");

        //Then
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var pack = await TestsCommon.DeserializeResponse<ImagePackDto>(getResponse);
        Assert.Equal(request.Description, pack!.Description);
        Assert.Equal(request.Name, pack.Name);
    }

    [Fact]
    public async Task UpdateImagePack()
    {
        //Given
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var updateRequest = new UpdateImagePackRequest()
        {
            Description = "Updated Test Image Pack Description."
        };

        var content = TestsCommon.CreateHttpContent(request);
        var updateContent = TestsCommon.CreateHttpContent(updateRequest);

        //When
        var response = await _client.PostAsync("/api/imagepacks", content);
        var idObject = await TestsCommon.DeserializeResponse<IdResponse>(response);
        var updateResponse = await _client.PatchAsync($"/api/imagepacks/{idObject.Id}", updateContent);
        var getResponse = await _client.GetAsync($"/api/imagepacks/{idObject!.Id}");

        //Then
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var pack = await TestsCommon.DeserializeResponse<ImagePackDto>(getResponse);
        Assert.Equal(updateRequest.Description, pack!.Description);
        Assert.Equal(request.Name, pack.Name);
    }

    [Fact]
    public async Task GetImagePacks()
    {
        //Given
        var request1 = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };
        var request2 = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var content1 = TestsCommon.CreateHttpContent(request1);
        var content2 = TestsCommon.CreateHttpContent(request2);

        //When
        var response1 = await _client.PostAsync("/api/imagepacks", content1);
        var response2 = await _client.PostAsync("/api/imagepacks", content2);

        var getResponse = await _client.GetAsync($"/api/imagepacks");

        var packs = await TestsCommon.DeserializeResponse<ImagePacksDto>(getResponse);

        //Then
        Assert.Equal(HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.True(packs!.Items.Count >= 2);

        Assert.Contains(packs.Items, x => x.Name == request1.Name);
        Assert.Contains(packs.Items, x => x.Name == request2.Name);
    }

    [Fact]
    public async Task DeleteImagePack()
    {
        //Given
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var content = TestsCommon.CreateHttpContent(request);

        //When
        var response = await _client.PostAsync("/api/imagepacks", content);
        var id = await TestsCommon.DeserializeResponse<IdResponse>(response);
        var deleteResponse = await _client.DeleteAsync($"/api/imagepacks/{id!.Id}");

        //Then
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
}
