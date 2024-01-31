using ImageHub.Api.Contracts.Thumbnails.GetThumbnail;
using ImageHub.Api.Infrastructure.Services;

namespace ImageHub.Api.Features.Thumbnails.GetThumbnail;

public class GetThumbnailHandler(IThumbnailRepository repository, ICacheService cacheService) 
    : IRequestHandler<GetThumbnailQuery, Result<GetThumbnailResponse>>
{
    public async Task<Result<GetThumbnailResponse>> Handle(GetThumbnailQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"thumbnail-{request.Id}";

        var cachedThumbnail = await cacheService.Get<GetThumbnailResponse>(cacheKey, cancellationToken);

        if (cachedThumbnail is not null)
        {
            return Result<GetThumbnailResponse>.Success(cachedThumbnail);
        }

        var thumbnail = await repository.GetThumbnail(request.Id, cancellationToken);

        if(thumbnail is null)
        {
            var error = GetThumbnailErrors.NotFound;
            return Result<GetThumbnailResponse>.Failure(error);
        }

        var response = new GetThumbnailResponse
        {
            Id = thumbnail.Id,
            Encoding = ThumbnailExtensions.BaseEncoding,
            EncodedImage = Convert.ToBase64String(thumbnail.Bytes),
            FileExtension = thumbnail.FileExtension,
            ImageId = thumbnail.ImageId,
            ProcessingStatus = thumbnail.ProcessingStatus,
            CreatedOnUtc = thumbnail.CreatedOnUtc,
            EditedAtUtc = thumbnail.EditedAtUtc
        };

        await cacheService.Set(cacheKey, response, TimeSpan.FromMinutes(5), cancellationToken);

        return Result<GetThumbnailResponse>.Success(response);
    }
}
