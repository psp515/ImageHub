using FluentValidation;
using ImageHub.Api.Infrastructure.Repositories;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class AddImagePackHandler : IRequestHandler<AddImagePackCommand, Result<Guid>>
{
    private readonly IImagePackRepository _repository;
    private readonly IValidator<AddImagePackCommand> _validator;

    public AddImagePackHandler(IImagePackRepository repository, IValidator<AddImagePackCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(AddImagePackCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var error = AddImagePackErrors.ValidationFailed(validationResult);
            return Result<Guid>.Failure(error);
        }

        var imagePackExists = await _repository.ExistsByName(request.Name, cancellationToken);

        if (imagePackExists)
            return Result<Guid>.Failure(AddImagePackErrors.ImagePackExist);

        var imagePack = new ImagePack
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CreatedOnUtc = DateTime.UtcNow,
            EditedAtUtc = DateTime.UtcNow
        };

        await _repository.AddImagePack(imagePack, cancellationToken);

        return Result<Guid>.Success(imagePack.Id);
    }
}
