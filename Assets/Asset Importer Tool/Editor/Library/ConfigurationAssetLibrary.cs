// Libraries
using System.Collections.Generic;
using UnityEngine;

// Namespace
namespace AssetImporterToolkit
{
    // Configuration Asset Library
    [CreateAssetMenu(fileName = "Asset Library", menuName = "Asset Importer/create/Config Asset Library")]
    public class ConfigurationAssetLibrary : ScriptableObject
    {
        // Creating and initializing a new configuration assets path
        public List<string> configurationAssetPathLibrary = new List<string>();

        // This function adds configuration asset path to the library
        public void AddConfigurationAssetPathToLibrary(string assetPath)
        {
            // Checking if Guid doesn't exist
            if (!configurationAssetPathLibrary.Contains(assetPath))
            {
                // Adding asset path to the library
                configurationAssetPathLibrary.Add(assetPath);
            }
            else
            {
                // Log a new warning message
                Debug.Log("The asset path guid : " + assetPath + " already exist in the configuration asset library");
            }
        }

        // Get all Configuration Asset Path
        public List<string> GetAllConfigurationAssetPath()
        {
            // Returning all
            return configurationAssetPathLibrary;
        }
    }
}