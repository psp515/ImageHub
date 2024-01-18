using ImageHub.Api.Contracts.Image.GetImage;

namespace ImageHub.Api.Features.Images.GetImage
{
    public class GetImageQuery : IRequest<Result<GetImageResponse>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
