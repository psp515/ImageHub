namespace ImageHub.Api.Infrastructure.Repositories;

public interface IImagePackRepository
{
    Task<bool> ExistsByName(string name, CancellationToken cancellationToken);
    Task AddImagePack(ImagePack imagePack, CancellationToken cancellationToken);
}