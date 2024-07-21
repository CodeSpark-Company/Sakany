namespace Sakany.Domain.Constants.Media
{
    public static class Image
    {
        public static string[] AllowedExtensions { get; } = [".jpg", ".jpeg", ".png"];
        public static string[] AllowedMimeTypes { get; } = ["image/jpeg", "image/png"];
        public const long MaxSizeInBytes = 5 * 1024 * 1024; // 5 MB
    }

    public static class Document
    {
        public static string[] AllowedExtensions { get; } = [".pdf"];
        public static string[] AllowedMimeTypes { get; } = ["application/pdf"];
        public const long MaxSizeInBytes = 5 * 1024 * 1024; // 5 MB
    }
}