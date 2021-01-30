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
        public static void OnReimportAssetsAtPath(string assetsPath)
        {
            // Getting asset file entries from the asset import directory class
            string[] assetFilesEntries = AssetImportDirectory.GetMultipleExtensionFileEntries(assetsPath);

            // Checking if there were file entries found in the directories
            if (assetFilesEntries.Length > 0)
            {
                // Looping through found assets
                for (int i = 0; i < assetFilesEntries.Length; i++)
                {
                    // Reimport asset
                    AssetDatabase.ImportAsset(assetFilesEntries[i]);

                    // Log
                    Debug.Log("Updated asset : " + assetFilesEntries[i]);
                }
            }
            else
            {
                // Log
                Debug.Log("No assets to update at path : " + assetsPath);

                // Returning from this function
                return;
            }

        }
    }
}