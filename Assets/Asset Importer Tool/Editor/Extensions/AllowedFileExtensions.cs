// Namespace
namespace AssetImporterToolkit
{
    // Allowed file extensions class
    public static class AllowedFileExtension
    {
        // An array of Unity supported audio extensions
        public static string[] AudioFileExtensions = new string[] { ".aif", ".wav", ".mp3", ".ogg" };

        // An array of Unity supported texture extensions
        public static string[] TextureFileExtensions = new string[] { ".psd", ".tiff", ".jpg", ".tga", "png", "gif", "bmp", "iff", ".pict" };

        // All the allowed file extensions combined
        public static string[] FileExtensions = new string[] { ".aif", ".wav", ".mp3", ".ogg", ".psd", ".tiff", ".jpg", ".tga", "png", "gif", "bmp", "iff", ".pict" };

        // A configuration asset allowed file extension
        public static string ConfigurationAssetExtension = ".asset";
    }
}   