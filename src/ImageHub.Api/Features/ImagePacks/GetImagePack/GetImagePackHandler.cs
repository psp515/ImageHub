using ImageHub.Api.Contracts.ImagePacks.GetImagePack;
using ImageHub.Api.Contracts.Thumbnails.GetThumbnail;
using ImageHub.Api.Infrastructure.Services;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackHandler(IImagePackRepository repository, ICacheService cacheService) 
    : IRequestHandler<GetImagePackQuery, Result<GetImagePackResponse>>
{
    public async Task<Result<GetImagePackResponse>> Handle(GetImagePackQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"thumbnail-{request.Id}";

        var cachedPack = await cacheService.Get<GetImagePackResponse>(cacheKey, cancellationToken);

        if (cachedPack is not null)
        {
            return Result<GetImagePackResponse>.Success(cachedPack);
        }

        var imagePack = await repository.GetImagePackById(request.Id, cancellationToken);

        if(imagePack is null)
        {
            return Result<GetImagePackResponse>.Failure(GetImagePackErrors.ImagePackNotFound);
        }

        var response = new GetImagePackResponse
        {
            Id = imagePack.Id,
            Name = imagePack.Name,
            Description = imagePack.Description,
            CreatedOnUtc = imagePack.CreatedOnUtc,
            EditedAtUtc = imagePack.EditedAtUtc
        };

        await cacheService.Set(cacheKey, response, TimeSpan.FromMinutes(5), cancellationToken);

        return Result<GetImagePackResponse>.Success(response);
    }
}
