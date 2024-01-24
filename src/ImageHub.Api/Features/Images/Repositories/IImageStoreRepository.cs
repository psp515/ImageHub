namespace ImageHub.Api.Features.Images.Repositories;

public interface IImageStoreRepository
{
    Task<byte[]> LoadImage(string path);
    Task<bool> DeleteImage(string path);
    Task<string> SaveImage(IFormFile file);
}
