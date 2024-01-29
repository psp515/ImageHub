namespace ImageHub.Api.Features.Images.AddImage;

public record AddImageEvent(Guid ThumbnailId, string imageKey);
