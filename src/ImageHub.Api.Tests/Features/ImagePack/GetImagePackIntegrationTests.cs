using ImageHub.Api.Tests.Features.ImagePack.Models;
using ImageHub.Api.Tests.Shared.Responses;
using System.Net;

namespace ImageHub.Api.Tests.Features.ImagePack;

public class GetImagePackIntegrationTests(IntegrationTestWebAppFactory factory) : BaseImagePackIntegrationTests(factory)
{
    [Fact]
    public async Task GetImagePack()
    {
        //Arrange
        var name = Guid.NewGuid().ToString();
        var description = "description";

        //Act
        var response = await AddImagePackWithData(name, description);
        var idObject = await TestsCommon.Deserialize<IdResponse>(response);
        var getResponse = await _client.GetAsync($"/api/imagepacks/{idObject!.Id}");
        var pack = await TestsCommon.Deserialize<ImagePackDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(description, pack!.Description);
        Assert.Equal(name, pack.Name);
    }

    [Fact]
    public async Task GetUnexistingImagePackFails()
    {
        //Arrange
        var name = Guid.NewGuid().ToString();
        var description = "description";
        var notExistingItem = Guid.NewGuid();

        //Act
        var response = await AddImagePackWithData(name, description);
        var getResponse = await _client.GetAsync($"/api/imagepacks/{notExistingItem}");

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}
