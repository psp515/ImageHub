using FluentValidation;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageValidator : AbstractValidator<AddImageCommand>
{
    private static readonly string[] AllowedFileTypes = 
    { 
        "image/jpeg", 
        "image/png", 
        "image/svg" 
    };

    public AddImageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Image)
            .NotNull();

        RuleFor(x => x.FileType)
            .Must(x => AllowedFileTypes.Contains(x))
            .WithMessage($"Invalid file type. Allowed types: {string.Join(',',AllowedFileTypes)}");
    }
}
