// Libraries
using UnityEngine;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Configuration asset creator
    public class ConfigurationAssetCreator : Editor
    {
        // Create a new scriptable object import configuration file.
        [MenuItem("24 Bit Games/Asset Importer/Create Configuration Asset")]
        private static void CreateImportConfiguration()
        {
            // Creating a new importer configuration instance.
            ConfigurationAsset assetImportConfigurationAsset = CreateInstance<ConfigurationAsset>();

            // Getting the path to save newely created scriptable object import configuration asset.
            string importConfigurationAssetSavePath = EditorUtility.SaveFilePanelInProject("Asset Importer", "Asset Import Configuration", "asset", "");

            // Checking if the configuration asset save path exist.
            bool configurationAssetSavePathExist = !string.IsNullOrEmpty(importConfigurationAssetSavePath);

            // Checking if the importer configuration asset's instance was created
            bool isConfigurationAssetCreated = assetImportConfigurationAsset != null;

            // Check if the importer configuration instance and the asset save path exist.
            if (isConfigurationAssetCreated && configurationAssetSavePathExist)
            {
                // Getting the selected import configuration asset directory where the configuration asset is created.
                string configurationAssetCreationDirectory = AssetImportDirectory.GetAssetDirectory(importConfigurationAssetSavePath);

                // Creating a new scriptable object import configuration asset to the import configuration path.
                AssetDatabase.CreateAsset(assetImportConfigurationAsset, importConfigurationAssetSavePath);

                // Initializing the newly created import configuration asset and assigning included directories.
                Configurations.AddConfigurationIncludedAssetDirectory(assetImportConfigurationAsset, configurationAssetCreationDirectory);
            }
            else
            {
                // Logging a new waning message to the console when this function is cancled by the user.
                Debug.LogWarning("Asset importer toolkit : Configuration file creation operation canceled/failed: Import configuration asset file not created");

                // Return from function
                return;
            }
        }
    }
}