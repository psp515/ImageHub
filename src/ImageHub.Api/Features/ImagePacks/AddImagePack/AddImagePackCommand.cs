namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
