using FluentValidation;
using ImageHub.Api.Contracts.Image.AddImage;

namespace ImageHub.Api.Features.Images.AddImage;

public class AddImageHandler(IImageRepository repository, IValidator<AddImageCommand> validator) 
    : IRequestHandler<AddImageCommand, Result<AddImageResponse>>
{
    public async Task<Result<AddImageResponse>> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var error = AddImageErrors.ValidationFailed(validationResult);
            return Result<AddImageResponse>.Failure(error);
        }

        var exists = await repository.ExistsByName(request.Name, cancellationToken);

        if (!exists)
        {
            var error = AddImageErrors.ImagePackExist;
            return Result<AddImageResponse>.Failure(error);
        }

        var image = new Image
        {
            Id = new Guid(),
            Name = request.Name,

        };

        //TODO : Solve Storage, Solve File Extensions


        var result = new AddImageResponse
        {
            Id = new Guid()
        };

        return Result<AddImageResponse>.Success(result);
    }
}
