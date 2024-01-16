namespace ImageHub.Api.Features.Images;

public interface IFileRepository
{
    Task<IFormFile> LoadImage(IFormFile file);
    Task<bool> SaveImage(string fullPath, IFormFile file);
    Task<bool> RemoveImage(string fullPath);
}
