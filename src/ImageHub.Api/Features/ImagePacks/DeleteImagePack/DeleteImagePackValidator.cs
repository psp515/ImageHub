using FluentValidation;

namespace ImageHub.Api.Features.ImagePacks.DeleteImagePack;

public class DeleteImagePackValidator : AbstractValidator<DeleteImagePackCommand>
{
    public DeleteImagePackValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
