// Libraries
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Asset import preprocessor class
    public static class AssetsReimporter
    { 
        // Re import assets at path
        public static void OnReimportAssetsAtPath(string assetsPath, ConfigurationAsset configurationAsset)
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
                        Debug.Log("Asset at path : " + importableAsset + " has been updated successfully.");
                    }
                }
            }
            else
            {
                // Checking if debug is enabled
                if (configurationAsset.AllowDebug)
                {
                    // Log
                    Debug.Log("No assets to update at path : " + assetsPath);
                }

                // Returning from this function
                return;
            }

        }
    }
}