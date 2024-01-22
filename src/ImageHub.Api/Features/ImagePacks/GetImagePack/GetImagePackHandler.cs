using ImageHub.Api.Contracts.ImagePacks.GetImagePack;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackHandler(IImagePackRepository repository) : IRequestHandler<GetImagePackQuery, Result<GetImagePackResponse>>
{
    public async Task<Result<GetImagePackResponse>> Handle(GetImagePackQuery request, CancellationToken cancellationToken)
    {
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
