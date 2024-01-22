namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackHandler(IImagePackRepository repository) : IRequestHandler<UpdateImagePackCommand, Result>
{
    public async Task<Result> Handle(UpdateImagePackCommand request, CancellationToken cancellationToken)
    {
        var guid = Guid.Parse(request.Id);

        var imagePack = await repository.GetImagePackById(guid, cancellationToken);

        if (imagePack is null)
        {
            var error = UpdateImagePackErrors.NotFound;
            return Result.Failure(error);
        }

        imagePack.Description = request.Description;

        await repository.UpdateImagePack(imagePack, cancellationToken);

        return Result.Success();
    }
}
