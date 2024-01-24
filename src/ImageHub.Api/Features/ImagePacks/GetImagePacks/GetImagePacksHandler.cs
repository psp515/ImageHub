using ImageHub.Api.Contracts.ImagePacks.GetImagePacks;
using ImageHub.Api.Features.ImagePacks.GetImagePacks;
using Mapster;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePacksHandler(IImagePackRepository repository) : IRequestHandler<GetImagePacksQuery, Result<GetImagePacksResponse>>
{
    public async Task<Result<GetImagePacksResponse>> Handle(GetImagePacksQuery request, CancellationToken cancellationToken)
    {
        var imagePacks = await repository.GetImagePacks(request.Page, request.Size, cancellationToken);

        if(imagePacks == null || imagePacks.Count == 0)
        {
            var error = GetImagePacksErrors.ImagePacksNotFound;
            return Result<GetImagePacksResponse>.Failure(error);
        }

        var dtos = imagePacks.Select(x => x.Adapt<ImagePackDto>()).ToList();
        
        var response = new GetImagePacksResponse
        {
            Items = dtos
        };

        return Result<GetImagePacksResponse>.Success(response);
    }
}
