using ImageHub.Api.Tests.Shared.Responses;
using System.Net;

namespace ImageHub.Api.Tests.Features.ImagePack;

public class AddImagePackIntegrationTests(IntegrationTestWebAppFactory factory) : BaseImagePackIntegrationTests(factory)
{
    [Fact]
    public async Task AddImagePack()
    {
        //Arrange

        //Act
        var response = await AddImagePackWithData();

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var id = await TestsCommon.Deserialize<IdResponse>(response);
        Assert.NotNull(id);
        Assert.True(Guid.Empty != id.Id);
    }

    [Fact]
    public async Task AddImagePackConflict()
    {
        //Arrange
        var name = $"Test {Guid.NewGuid()}";


        //Act
        var response = await AddImagePackWithData(name:name);
        var copyResponse = await AddImagePackWithData(name:name);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var id = await TestsCommon.Deserialize<IdResponse>(response);
        Assert.NotNull(id);
        Assert.True(Guid.Empty != id.Id);
        Assert.Equal(HttpStatusCode.Conflict, copyResponse.StatusCode);
    }

    [Fact]
    public async Task AddImagePackWithInvalidName()
    {
        //Arrange
        var name = $"Test {Guid.NewGuid()} {Guid.NewGuid()}";

        //Act
        var response = await AddImagePackWithData(name);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AddImagePackWithInvalidDescription()
    {
        //Arrange
        var description = new string('*', 600);

        //Act
        var response = await AddImagePackWithData(description:description);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

