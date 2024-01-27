using ImageHub.Api.Contracts.Thumbnails.GetThumbnail;

namespace ImageHub.Api.Features.Thumbnails.GetThumbnail;

public class GetThumbnailHandler(IThumbnailRepository repository) : IRequestHandler<GetThumbnailQuery, Result<GetThumbnailResponse>>
{
    public async Task<Result<GetThumbnailResponse>> Handle(GetThumbnailQuery request, CancellationToken cancellationToken)
    {
        var thumbnail = await repository.GetThumbnail(request.Id, cancellationToken);

        if(thumbnail is null)
        {
            var error = GetThumbnailErrors.NotFound;
            return Result<GetThumbnailResponse>.Failure(error);
        }

        var response = new GetThumbnailResponse
        {
            Id = thumbnail.Id,
            Bytes = thumbnail.Bytes,
            FileExtension = thumbnail.FileExtension,
            ImageId = thumbnail.ImageId,
            ProcessingStatus = thumbnail.ProcessingStatus,
            CreatedOnUtc = thumbnail.CreatedOnUtc,
            EditedAtUtc = thumbnail.EditedAtUtc
        };

        return Result<GetThumbnailResponse>.Success(response);
    }
}
