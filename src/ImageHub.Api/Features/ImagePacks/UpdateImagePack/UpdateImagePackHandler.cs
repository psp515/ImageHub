using ImageHub.Api.Contracts.ImagePacks.UpdateImagePack;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackHandler(IImagePackRepository repository) : IRequestHandler<UpdateImagePackCommand, Result<UpdateImagePackResponse>>
{
    public async Task<Result<UpdateImagePackResponse>> Handle(UpdateImagePackCommand request, CancellationToken cancellationToken)
    { 
        var imagePack = await repository.GetImagePackById(request.Id, cancellationToken);

        if (imagePack is null)
        {
            var error = UpdateImagePackErrors.NotFound;
            return Result<UpdateImagePackResponse>.Failure(error);
        }

        imagePack.Description = request.Description;
        imagePack.EditedAtUtc = DateTime.UtcNow;

        await repository.UpdateImagePack(imagePack, cancellationToken);

        var response = new UpdateImagePackResponse
        {
            Id = request.Id,
        };

        return Result<UpdateImagePackResponse>.Success(response);
    }
}
