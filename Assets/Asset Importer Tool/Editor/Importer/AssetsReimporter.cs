// Used libraries.
using UnityEditor;

// Namespace.
namespace AssetImporterToolkit
{
    // Asset import preprocessor static class.
    public static class AssetsReimporter
    {
        // Class log name is used for logging messages to the debugger Logs.
        private static string classLogName = "Assets Reimporter";

        /// <summary>
        /// Re-imports assets from a given asset path.
        /// </summary>
        /// <param name="assetsPath">Takes in a string object called asset path as the assets to re-import from directory path.</param>
        /// <param name="configurationAsset">Takes in a configuration asset file used to configure re-imported assets.</param>
        public static void ReimportAssetsAtPath(string assetsPath, ConfigurationAsset configurationAsset)
        {
            // Searching the asset folders to find importable assts that can be retroactively updated.
            string[] importableAssetFilesEntries = Utilities.GetMultipleExtensionFileEntries(assetsPath);

            // Checking if there were file entries found in the directories.
            bool importableAssetsFound = importableAssetFilesEntries.Length > 0;

            // Checking if assets were found.
            if (importableAssetsFound)
            {
                // Going through found assets in the directory.
                foreach (string importableAsset in importableAssetFilesEntries)
                {
                    // Reimport asset.
                    AssetDatabase.ImportAsset(importableAsset);

                    // Checking if configuration aaset's debug is enabled.
                    if (configurationAsset.AllowDebug)
                    {
                        // Message to log in the unity debug console.
                        string logMessage = "Asset at path : " + importableAsset + " has been updated successfully.";

                        // Log a new message to the unity debug console.
                        Debugger.Log(className : classLogName, message : logMessage);
                    }
                }
            }
            else
            {   
                // Checking if configuration aaset's debug is enabled.
                if (configurationAsset.AllowDebug)
                {
                    // Message to log in the unity debug console.
                    string logMessage = "No assets to update at path : " + assetsPath;

                    // Logging a new message to the unity debug console.
                    Debugger.Log(className: classLogName, message: logMessage);
                }

                return;
            }

        }
    }
}