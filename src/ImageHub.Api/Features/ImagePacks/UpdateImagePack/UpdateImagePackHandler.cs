using FluentValidation;
using ImageHub.Api.Infrastructure.Repositories;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class UpdateImagePackHandler(IImagePackRepository repository, IValidator<UpdateImagePackCommand> validator) : IRequestHandler<UpdateImagePackCommand, Result>
{
    private readonly IImagePackRepository _repository = repository;
    private readonly IValidator<UpdateImagePackCommand> _validator = validator;

    public async Task<Result> Handle(UpdateImagePackCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var error = UpdateImagePackErrors.ValidationFailed(validationResult);
            return Result.Failure(error);
        }

        var imagePack = await _repository.GetImagePackByIdAsync(request.Id, cancellationToken);

        if (imagePack is null)
        {
            var error = UpdateImagePackErrors.NotFound;
            return Result.Failure(error);
        }

        imagePack.Description = request.Description;

        await _repository.UpdateImagePack(imagePack, cancellationToken);

        return Result.Success();
    }
}
