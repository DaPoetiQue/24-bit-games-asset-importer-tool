// Namespace.
namespace AssetImporterToolkit
{
    /// <summary>
    /// This class contains a list of supported or allowed file extensions.
    /// </summary>
    public static class AllowedFileExtension
    {
        /// <summary>
        /// An array of supported or allowed unity audio file extensions.
        /// </summary>
        public static string[] AudioFileExtensions = new string[] { ".aif", ".wav", ".mp3", ".ogg" };

        /// <summary>
        /// An array of Unity supported texture extensions.
        /// </summary>
        public static string[] TextureFileExtensions = new string[] { ".psd", ".tiff", ".jpg", ".tga", "png", "gif", "bmp", "iff", ".pict" };

        /// <summary>
        /// An array consisting of all the allowed file extensions combined.
        /// </summary>
        public static string[] FileExtensions = new string[] { ".aif", ".wav", ".mp3", ".ogg", ".psd", ".tiff", ".jpg", ".tga", "png", "gif", "bmp", "iff", ".pict" };

        /// <summary>
        /// A configuration scriptable object asset allowed file extension.
        /// </summary>
        public static string ConfigurationAssetExtension = ".asset";
    }
}   