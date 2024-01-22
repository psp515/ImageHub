namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackHandler(IImagePackRepository repository) : IRequestHandler<DeleteImagePackCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteImagePackCommand request, CancellationToken cancellationToken)
    {
        var guid = Guid.Parse(request.Id);

        var imagePack = await repository.GetImagePackById(guid, cancellationToken);

        if (imagePack is null)
        {
            var error = DeleteImagePackErrors.NotFound;
            return Result<Guid>.Failure(error);
        }

        await repository.DeleteImagePack(imagePack, cancellationToken);

        return Result<Guid>.Success(imagePack.Id);
    }
}
