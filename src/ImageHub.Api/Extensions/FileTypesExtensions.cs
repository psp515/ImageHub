using ImageHub.Api.Features.ImagePacks;

namespace ImageHub.Api.Extensions;

public static class FileTypesExtensions
{
    public static string FileTypeName(this FileTypes type) => type switch
    {
        FileTypes.Png  => "png",
        FileTypes.Svg  => "svg",
        FileTypes.Jpeg => "jpeg",
        FileTypes.Jpg  => "jpg",
        _              => "unallowed"
    };

    public static FileTypes FileTypeFromName(this string name) => name switch
    {
        "png"  => FileTypes.Png,
        "svg"  => FileTypes.Svg,
        "jpeg" => FileTypes.Jpeg,
        "jpg"  => FileTypes.Jpg,
        _      => FileTypes.Invalid
    };
}
