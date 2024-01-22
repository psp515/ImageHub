namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackHandler(IImagePackRepository repository) : IRequestHandler<AddImagePackCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddImagePackCommand request, CancellationToken cancellationToken)
    {
        var imagePackExists = await repository.ExistsByName(request.Name, cancellationToken);

        if (imagePackExists)
            return Result<Guid>.Failure(AddImagePackErrors.ImagePackExist);

        var imagePack = new ImagePack
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CreatedOnUtc = DateTime.UtcNow,
            EditedAtUtc = DateTime.UtcNow
        };

        await repository.AddImagePack(imagePack, cancellationToken);

        return Result<Guid>.Success(imagePack.Id);
    }
}
