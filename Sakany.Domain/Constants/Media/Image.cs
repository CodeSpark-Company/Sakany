namespace Sakany.Domain.Constants.Media
{
    public static class Image
    {
        public static string[] AllowedExtensions { get; } = [".jpg", ".jpeg", ".png"];
        public static string[] AllowedMimeTypes { get; } = ["image/jpeg", "image/png"];
        public const long MaxSizeInBytes = 5 * 1024 * 1024; // 5 MB
    }
}