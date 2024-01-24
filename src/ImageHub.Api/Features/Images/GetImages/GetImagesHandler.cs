using ImageHub.Api.Features.Images.GetImages;
using ImageHub.Api.Features.Images.Repositories;

namespace ImageHub.Api.Features.Images.GetImagesByPack;

public class GetImagesHandler(IImageRepository repository) : IRequestHandler<GetImagesQuery, Result<List<Image>>>
{
    public async Task<Result<List<Image>>> Handle(GetImagesQuery request, CancellationToken cancellationToken)
    {
        var images = await repository.GetImages(request.PackId, request.Page, request.Size, cancellationToken);

        if (images == null || images.Count == 0)
        {
            var error = GetImagesError.ImagesNotFound;
            return Result<List<Image>>.Failure(error);
        }

        return Result<List<Image>>.Success(images);
    }
}
