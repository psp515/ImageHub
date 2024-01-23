namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; } = Guid.Empty;
}
