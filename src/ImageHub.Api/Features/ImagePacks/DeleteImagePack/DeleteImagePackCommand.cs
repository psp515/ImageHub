namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackCommand : IRequest<Result<Guid>>
{
    public string Id { get; set; } = string.Empty;
}
