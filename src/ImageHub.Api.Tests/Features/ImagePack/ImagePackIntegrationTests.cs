using ImageHub.Api.Contracts.ImagePacks.AddImagePack;
using ImageHub.Api.Contracts.ImagePacks.UpdateImagePack;
using ImageHub.Api.Tests.Features.ImagePack.Models;
using ImageHub.Api.Tests.Models;
using System.Net;

namespace ImageHub.Api.Tests.Features.ImagePack;

public class ImagePackIntegrationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task AddImagePack()
    {
        //Arrange
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var content = TestsCommon.CreateHttpContent(request);

        //Act
        var response = await _client.PostAsync("/api/imagepacks", content);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task FailAddImagePackWithSameName()
    {
        //Arrange
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var content = TestsCommon.CreateHttpContent(request);

        //Act
        var response = await _client.PostAsync("/api/imagepacks", content);
        var copyResponse = await _client.PostAsync("/api/imagepacks", content);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, copyResponse.StatusCode);
    }

    [Fact]
    public async Task GetImagePack()
    {
        //Arrange
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var content = TestsCommon.CreateHttpContent(request);

        //Act
        var response = await _client.PostAsync("/api/imagepacks", content);
        var idObject = await TestsCommon.DeserializeResponse<IdResponse>(response);
        var getResponse = await _client.GetAsync($"/api/imagepacks/{idObject!.Id}");
        var pack = await TestsCommon.DeserializeResponse<ImagePackDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(request.Description, pack!.Description);
        Assert.Equal(request.Name, pack.Name);
    }

    [Fact]
    public async Task UpdateImagePack()
    {
        //Arrange
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

        //Act
        var response = await _client.PostAsync("/api/imagepacks", content);
        var idObject = await TestsCommon.DeserializeResponse<IdResponse>(response);
        var updateResponse = await _client.PatchAsync($"/api/imagepacks/{idObject.Id}", updateContent);
        var getResponse = await _client.GetAsync($"/api/imagepacks/{idObject!.Id}");
        var pack = await TestsCommon.DeserializeResponse<ImagePackDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(updateRequest.Description, pack!.Description);
        Assert.Equal(request.Name, pack.Name);
    }

    [Fact]
    public async Task GetImagePacks()
    {
        //Arrange
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

        //Act   
        var response1 = await _client.PostAsync("/api/imagepacks", content1);
        var response2 = await _client.PostAsync("/api/imagepacks", content2);

        var getResponse = await _client.GetAsync($"/api/imagepacks");

        var packs = await TestsCommon.DeserializeResponse<ImagePacksDto>(getResponse);

        //Assert
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
        //Arrange
        var request = new AddImagePackRequest()
        {
            Name = $"Test {Guid.NewGuid()}",
            Description = "Test Image Pack Description."
        };

        var content = TestsCommon.CreateHttpContent(request);

        //Act
        var response = await _client.PostAsync("/api/imagepacks", content);
        var id = await TestsCommon.DeserializeResponse<IdResponse>(response);
        var deleteResponse = await _client.DeleteAsync($"/api/imagepacks/{id!.Id}");

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
}
