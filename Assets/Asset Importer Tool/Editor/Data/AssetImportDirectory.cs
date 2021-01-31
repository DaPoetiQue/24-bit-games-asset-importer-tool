using System.IO;
using UnityEditor;

namespace AssetImporterToolkit
{
    public static class AssetImportDirectory
    {
        public static string GetAssetDirectory(string path) => Path.GetDirectoryName(path);

        public static string[] FindImportConfigurations(string filter) => AssetDatabase.FindAssets(filter);

        public static bool IsValidConfigurationAsset(ConfigurationAsset configurationAsset)
        {
            return configurationAsset != null;
        }
    }
}