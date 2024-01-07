
namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class EditImagePackCommand : IRequest<Result>
{
    public string Description { get; set; } = string.Empty;
    public Guid Id { get; set; }
}
