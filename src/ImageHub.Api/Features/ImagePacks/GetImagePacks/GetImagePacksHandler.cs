using ImageHub.Api.Features.ImagePacks.GetImagePacks;
using ImageHub.Api.Infrastructure.Repositories;
using Mapster;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePacksHandler(IImagePackRepository repository) : IRequestHandler<GetImagePacksQuery, Result<GetImagePacksResponse>>
{
    private readonly IImagePackRepository _repository = repository;

    public async Task<Result<GetImagePacksResponse>> Handle(GetImagePacksQuery request, CancellationToken cancellationToken)
    {
        var imagePacks = await _repository.GetImagePacksAsync(cancellationToken);

        var dtos = imagePacks.Select(x => x.Adapt<ImagePackDto>()).ToList();
        
        var response = new GetImagePacksResponse
        {
            Items = dtos
        };

        return Result<GetImagePacksResponse>.Success(response);
    }
}
