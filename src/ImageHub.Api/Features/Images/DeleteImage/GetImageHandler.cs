using FluentValidation;
using ImageHub.Api.Contracts.Image.AddImage;
using ImageHub.Api.Contracts.Image.GetImage;
using ImageHub.Api.Features.Images.AddImage;
using ImageHub.Api.Features.Images.Repositories;

namespace ImageHub.Api.Features.Images.GetImage;

public class GetImageHandler(IImageRepository imageRepository, IValidator<GetImageQuery> validator) : IRequestHandler<GetImageQuery, Result<GetImageResponse>>
{
    public async Task<Result<GetImageResponse>> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = DeleteImageErrors.ValidationFailed(validationResult);
            return Result<GetImageResponse>.Failure(error);
        }

        var guid = Guid.Parse(request.Id);
        var image = await imageRepository.GetImageById(guid, cancellationToken);

        if (image is null)
        {
            var error = DeleteImageErrors.ImageNotFound;
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
