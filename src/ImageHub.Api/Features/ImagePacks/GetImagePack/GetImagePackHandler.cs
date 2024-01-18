using FluentValidation;
using ImageHub.Api.Contracts.ImagePacks.GetImagePack;
using ImageHub.Api.Features.ImagePacks.DeleteImagePack;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackHandler(IImagePackRepository repository, IValidator<GetImagePackQuery> validator) : IRequestHandler<GetImagePackQuery, Result<GetImagePackResponse>>
{
    public async Task<Result<GetImagePackResponse>> Handle(GetImagePackQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var error = DeleteImagePackErrors.ValidationFailed(validationResult);
            return Result<GetImagePackResponse>.Failure(error);
        }

        var guid = Guid.Parse(request.Id);

        var imagePack = await repository.GetImagePackById(guid, cancellationToken);

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

        return Result<GetImagePackResponse>.Success(response);
    }
}
