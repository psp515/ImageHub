namespace ImageHub.Api.Features.Thumbnails.GetThumbnails;

public class GetThumbnailsHandler(IThumbnailRepository repository) : IRequestHandler<GetThumbnailsQuery, Result<List<Thumbnail>>>
{
    public async Task<Result<List<Thumbnail>>> Handle(GetThumbnailsQuery request, CancellationToken cancellationToken)
    {
        var thumbnails = await repository.GetThumbnails(request.PackId, request.Page, request.Size, cancellationToken);

        if (thumbnails == null || thumbnails.Count == 0)
        {
            var error = GetThumbnailsError.ThumbnailsNotFound;
            return Result<List<Thumbnail>>.Failure(error);
        }

        return Result<List<Thumbnail>>.Success(thumbnails);
    }
}
