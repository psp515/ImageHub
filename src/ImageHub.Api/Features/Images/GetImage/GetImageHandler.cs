using ImageHub.Api.Contracts.Image.GetImage;
using ImageHub.Api.Features.Images.Repositories;
using ImageHub.Api.Infrastructure.Services;

namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageHandler(IImageRepository imageRepository, ICacheService cacheService) 
    : IRequestHandler<GetImageQuery, Result<GetImageResponse>>
{
    public async Task<Result<GetImageResponse>> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"thumbnail-{request.Id}";

        var cachedImage = await cacheService.Get<GetImageResponse>(cacheKey, cancellationToken);

        if (cachedImage is not null)
        {
            return Result<GetImageResponse>.Success(cachedImage);
        }

        var image = await imageRepository.GetImageById(request.Id, cancellationToken);

        if (image is null)
        {
            var error = GetImageErrors.ImageNotFound;
            return Result<GetImageResponse>.Failure(error);
        }

        var response = new GetImageResponse
        {
            Id = image.Id.ToString(),
            Name = image.Name,
            Description = image.Description,
            FileType = image.FileType,
            PackId = image.PackId?.ToString() ?? string.Empty,
            CreatedOnUtc = image.CreatedOnUtc,
            EditedAtUtc = image.EditedAtUtc
        };

        await cacheService.Set(cacheKey, response, TimeSpan.FromMinutes(5), cancellationToken);

        return Result<GetImageResponse>.Success(response);
    }
}
