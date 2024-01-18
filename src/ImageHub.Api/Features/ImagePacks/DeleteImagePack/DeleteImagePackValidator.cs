using FluentValidation;

namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackValidator : AbstractValidator<DeleteImagePackCommand>
{
    public DeleteImagePackValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Provided id is not valid id.");
    }
}
