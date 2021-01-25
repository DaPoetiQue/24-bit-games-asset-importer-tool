// Libraries
using System.IO;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Asset import directory class
    public class AssetImportDirectory
    {
        // Get Get Asset Directory
        public static string GetAssetDirectory(string path) => Path.GetDirectoryName(path);

        // Find Asset
        public static string[] FindImportConfigurations(string filter) => AssetDatabase.FindAssets(filter);
    }

}