using ImageHub.Api.Tests.Features.ImagePack.Models;
using System.Net;

namespace ImageHub.Api.Tests.Features.ImagePack;

public class GetImagePacksIntegrationTests(IntegrationTestWebAppFactory factory) : BaseImagePackIntegrationTests(factory)
{
    [Fact]
    public async Task GetImagePacks()
    {
        //Arrange
        var name1 = $"Test {Guid.NewGuid()}";
        var name2 = $"Test {Guid.NewGuid()}";

        //Act   
        var response1 = await AddImagePackWithData(name1);
        var response2 = await AddImagePackWithData(name2);

        var getResponse = await _client.GetAsync($"/api/imagepacks");

        var packs = await TestsCommon.Deserialize<ImagePacksDto>(getResponse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.True(packs!.Items.Count >= 2);

        Assert.Contains(packs.Items, x => x.Name == name1);
        Assert.Contains(packs.Items, x => x.Name == name2);
    }

    [Fact]
    public async Task GetSecondPageOfImagePacksFails()
    {
        //Arrange

        //Act   

        var getResponse = await _client.GetAsync($"/api/imagepacks?page={2}");

        var packs = await TestsCommon.Deserialize<ImagePacksDto>(getResponse);

        //Assert

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task GetImagePacksWithPagination()
    {
        //Arrange
        var name1 = $"Test {Guid.NewGuid()}";
        var name2 = $"Test {Guid.NewGuid()}";

        //Act   
        var response1 = await AddImagePackWithData(name1);
        var response2 = await AddImagePackWithData(name2);

        var getResponse = await _client.GetAsync($"/api/imagepacks?page=1&size=1");
        var get2Response = await _client.GetAsync($"/api/imagepacks?page=2&size=1");

        //Assert
        Assert.Equal(HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var packs = await TestsCommon.Deserialize<ImagePacksDto>(getResponse);
        Assert.True(packs!.Items.Count == 1);
        Assert.Equal(HttpStatusCode.OK, get2Response.StatusCode);
        var packs2 = await TestsCommon.Deserialize<ImagePacksDto>(get2Response);
        Assert.True(packs2!.Items.Count == 1);
    }
}
