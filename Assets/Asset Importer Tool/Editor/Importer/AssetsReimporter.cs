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
            // Searching the assets directory for files with supported extension
            var fileEntries = Directory.GetFiles(assetsPath).Where(file => AllowedFileExtension.FileExtensions.Any(file.ToLower().EndsWith));

            // Converting file entries to assets path
            string[] fileEntriesToPathArray = fileEntries.ToArray();

            // Checking if there were file entries found in the directories
            if(fileEntriesToPathArray.Length > 0)
            {
                // Looping through found assets
                for (int i = 0; i < fileEntriesToPathArray.Length; i++)
                {
                    // Log
                    Debug.Log("Updating asset : " + fileEntriesToPathArray[i]);
                }
            }
            else
            {
                // Log
                Debug.Log("No assets to update.");

                // Returning from this function
                return;
            }

        }
    }
}