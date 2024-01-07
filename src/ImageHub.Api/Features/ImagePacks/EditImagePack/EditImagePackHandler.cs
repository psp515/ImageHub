using FluentValidation;
using ImageHub.Api.Infrastructure.Repositories;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class EditImagePackHandler(IImagePackRepository repository, IValidator<EditImagePackCommand> validator) : IRequestHandler<EditImagePackCommand, Result>
{
    private readonly IImagePackRepository _repository = repository;
    private readonly IValidator<EditImagePackCommand> _validator = validator;

    public async Task<Result> Handle(EditImagePackCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var error = AddImagePackErrors.ValidationFailed(validationResult);
            return Result.Failure(error);
        }

        await _repository.EditImagePack(request, cancellationToken);

        return Result.Success();
    }
}
