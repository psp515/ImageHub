using FluentValidation;
using ImageHub.Api.Features.ImagePacks.GetImagePack;

namespace ImageHub.Api.Features.ImagePacks.AddImagePack;

public class GetImagePackValidator : AbstractValidator<GetImagePackQuery>
{
    public GetImagePackValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Provided id is not valid id.");
    }
}