using ImageHub.Api.Features.Images.Repositories;

namespace ImageHub.Api.Infrastructure.Repositories;

public class FileRepository(IConfiguration configuration) : IImageStoreRepository
{
   private readonly string _path = configuration.GetConnectionString("FileStorage") ?? throw new ArgumentException("Please specify connection string for file storage");

    public async Task<byte[]> LoadImage(string path)
    {
        if (string.IsNullOrEmpty(path))
            return [];

        try
        {
            var filePath = Path.Combine(_path, Path.GetRandomFileName());

            var bytes = await File.ReadAllBytesAsync(path);

            return bytes;
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> DeleteImage(string path)
    {
        if (string.IsNullOrEmpty(path))
            return false;

        try
        {
            var filePath = Path.Combine(_path, Path.GetRandomFileName());

            await Task.Run(() => File.Delete(path));

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> SaveImage(IFormFile file)
    {
        if (file.Length < 0)
            return string.Empty;

        try
        {
            var filePath = Path.Combine(_path, Path.GetRandomFileName());
            
            using var stream = File.Create(filePath);

            await file.CopyToAsync(stream);

            return filePath;
        }
        catch 
        {
            return string.Empty;
        }
    }
}
