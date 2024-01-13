using ImageHub.Api.Contracts.ImagePacks.GetImagePack;
using ImageHub.Api.Infrastructure.Repositories;

namespace ImageHub.Api.Features.ImagePacks.GetImagePack;

public class GetImagePackHandler(IImagePackRepository repository) : IRequestHandler<GetImagePackQuery, Result<GetImagePackResponse>>
{
    private readonly IImagePackRepository _repository = repository;

    public async Task<Result<GetImagePackResponse>> Handle(GetImagePackQuery request, CancellationToken cancellationToken)
    {
        var imagePack = await _repository.GetImagePackByIdAsync(request.Id, cancellationToken);

        if(imagePack is null)
        {
            return Result<GetImagePackResponse>.Failure(GetImagePackErrors.ImagePackNotFound);
        }

        var response = new GetImagePackResponse
        {
            Id = imagePack.Id,
            Name = imagePack.Name,
            Description = imagePack.Description,
            CreatedOnUtc = imagePack.CreatedOnUtc,
            EditedAtUtc = imagePack.EditedAtUtc
        };

        return Result<GetImagePackResponse>.Success(response);
    }
}
