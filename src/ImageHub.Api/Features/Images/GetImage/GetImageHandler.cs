using ImageHub.Api.Contracts.Image.GetImage;
using ImageHub.Api.Features.Images.Repositories;

namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageHandler(IImageRepository imageRepository) : IRequestHandler<GetImageQuery, Result<GetImageResponse>>
{
    public async Task<Result<GetImageResponse>> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var guid = Guid.Parse(request.Id);
        var image = await imageRepository.GetImageById(guid, cancellationToken);

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

        return Result<GetImageResponse>.Success(response);
    }
}
