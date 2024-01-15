using FluentValidation;

namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackHandler(IImagePackRepository repository, IValidator<DeleteImagePackCommand> validator) : IRequestHandler<DeleteImagePackCommand, Result<Guid>>
{
    private readonly IImagePackRepository _repository = repository;
    private readonly IValidator<DeleteImagePackCommand> _validator = validator;

    public async Task<Result<Guid>> Handle(DeleteImagePackCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var error = DeleteImagePackErrors.ValidationFailed(validationResult);
            return Result<Guid>.Failure(error);
        }

        var imagePack = await _repository.GetImagePackByIdAsync(request.Id, cancellationToken);

        if (imagePack is null)
        {
            var error = DeleteImagePackErrors.NotFound;
            return Result<Guid>.Failure(error);
        }

        await _repository.DeleteImagePack(imagePack, cancellationToken);

        return Result<Guid>.Success(imagePack.Id);
    }
}
