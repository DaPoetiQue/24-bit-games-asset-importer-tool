// Libraries
using System.IO;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Asset import directories class
    public static class AssetImportDirectory
    {
        // Getting a asset directory from a given path
        public static string GetAssetDirectory(string path) => Path.GetDirectoryName(path);

        // Finding existing import configuration asset using filters 
        public static string[] FindImportConfigurations(string filter) => AssetDatabase.FindAssets(filter);

        // This file is for checking the configuration asset is not null
        public static bool IsValidConfigurationAsset(ConfigurationAsset configurationAsset)
        {
            // Return true if configuration is not null
            return configurationAsset != null;
        }
    }
}