namespace Sakany.Domain.Constants.Media
{
    public static class Document
    {
        public static string[] AllowedExtensions { get; } = [".pdf"];
        public static string[] AllowedMimeTypes { get; } = ["application/pdf"];
        public const long MaxSizeInBytes = 5 * 1024 * 1024; // 5 MB
    }
}