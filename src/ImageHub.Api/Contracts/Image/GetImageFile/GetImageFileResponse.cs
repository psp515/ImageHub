namespace ImageHub.Api.Contracts.Image.GetImageFile;

public class GetImageFileResponse
{
    public byte[] Bytes { get; set;} = [];
    public string FileType { get; set; } = string.Empty;
    public DateTime EditedAtUtc { get; set; } = DateTime.MinValue;
}
