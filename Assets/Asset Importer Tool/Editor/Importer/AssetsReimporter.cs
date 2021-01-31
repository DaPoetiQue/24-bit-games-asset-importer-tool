// Libraries
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Asset import preprocessor class
    public static class AssetsReimporter
    {
        // Class log name
        private static string classLogName = "Assets Reimporter";

        // Re import assets at path
        public static void ReimportAssetsAtPath(string assetsPath, ConfigurationAsset configurationAsset)
        {
            // Searching the asset folders to find importable assts that can be retroactively updated.
            string[] importableAssetFilesEntries = Utilities.GetMultipleExtensionFileEntries(assetsPath);

            // Checking if there were file entries found in the directories
            bool importableAssetsFound = importableAssetFilesEntries.Length > 0;

            // Checking if assets were found
            if (importableAssetsFound)
            {
                // Going through found assets in the directory
                foreach (string importableAsset in importableAssetFilesEntries)
                {
                    // Reimport asset
                    AssetDatabase.ImportAsset(importableAsset);

                    // Checking if debug is enabled
                    if (configurationAsset.AllowDebug)
                    {
                        // Log a new message to the console
                        Debugger.Log(className : classLogName, message : " Asset at path : " + importableAsset + " has been updated successfully.");
                    }
                }
            }
            else
            {
                // Checking if debug is enabled
                if (configurationAsset.AllowDebug)
                {
                    // Log
                    Debugger.Log(className: classLogName, message: " No assets to update at path : " + assetsPath);
                }

                // Returning from this function
                return;
            }

        }
    }
}