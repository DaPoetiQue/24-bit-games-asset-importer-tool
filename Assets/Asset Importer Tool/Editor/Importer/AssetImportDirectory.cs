// Used libraries.
using System.IO;

// Namespace.
namespace AssetImporterToolkit
{
    /// <summary>
    /// This class encapsulate the asset importer directory functions.
    /// </summary>
    public static class AssetImportDirectory
    {
        /// <summary>
        /// This static function is used to get a directory containing the given asset path. 
        /// </summary>
        /// <param name="assetPath">Takes in a string object as a asset path.</param>
        /// <returns>Return a folder/directory name of the given asset path.</returns>
        public static string GetAssetDirectory(string assetPath) => Path.GetDirectoryName(assetPath);

        /// <summary>
        /// This function takes a configuration asset and checks if it's not null.
        /// </summary>
        /// <param name="configurationAsset">Takes in a configuration asset.</param>
        /// <returns>Return true if the configuration asset is not null or invalid.</returns>
        public static bool IsValidConfigurationAsset(ConfigurationAsset configurationAsset)
        {
            // Return true if configuration is not null.
            return configurationAsset != null;
        }
    }
}