using ImageHub.Api.Features.ImagePacks.GetImagePacks;
using ImageHub.Api.Infrastructure.Repositories;
using Mapster;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePacksHandler(IImagePackRepository repository) : IRequestHandler<GetImagePacksQuery, GetImagePacksResponse>
{
    private readonly IImagePackRepository _repository = repository;

    public async Task<GetImagePacksResponse> Handle(GetImagePacksQuery request, CancellationToken cancellationToken)
    {
        var imagePacks = await _repository.GetImagePacksAsync(cancellationToken);

        var dtos = imagePacks.Select(x => x.Adapt<ImagePackDto>()).ToList();
        
        return new GetImagePacksResponse
        {
            Items = dtos
        };
    }
}
