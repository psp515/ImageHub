using ImageHub.Api.Features.Images.Repositories;
using Microsoft.AspNetCore.Http;

namespace ImageHub.Api.Tests.Mocks;


/// <summary>
/// Class created due to problems with storing files in CI/CD pipeline.
/// </summary>
internal class ImageStoreMock : IImageStoreRepository
{
    public Dictionary<string, byte[]> Images { get; set; } = [];

    public Task<bool> DeleteImage(string path)
    {
        Images.Remove(path);

        return Task.FromResult(true);
    }

    public Task<byte[]> LoadImage(string path)
    {
        return Task.FromResult(Images[path]);
    }

    public async Task<string> SaveImage(IFormFile file)
    {
        var path = Guid.NewGuid().ToString();

        using var stream = new MemoryStream();

        await file.CopyToAsync(stream);

        Images.Add(path, stream.GetBuffer());

        return path;
    }
}
