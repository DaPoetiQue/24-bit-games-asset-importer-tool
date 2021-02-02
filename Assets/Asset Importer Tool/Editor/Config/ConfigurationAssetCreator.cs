// Used libraries.
using UnityEditor;

// Namespace.
namespace AssetImporterToolkit
{
    public class ConfigurationAssetCreator : Editor
    {
        // This log name is used when logging messages to the debbuger.
        private static string classLogName = "Configuration Asset Creator";

        // This function creates a new scriptable object import configuration asset file inside the project directory.
        [MenuItem("24 Bit Games/Asset Importer/Create Configuration Asset")]
        private static void CreateImportConfiguration()
        {
            // Creating a new importer configuration asset file instance.
            ConfigurationAsset assetImportConfigurationAsset = CreateInstance<ConfigurationAsset>();

            // Getting the path to save newely created scriptable object import configuration asset.
            string importConfigurationAssetSavePath = EditorUtility.SaveFilePanelInProject("Asset Importer", "Asset Import Configuration", "asset", "");

            // Checking if the configuration asset save path exist.
            bool configurationAssetSavePathExist = !string.IsNullOrEmpty(importConfigurationAssetSavePath);

            // Checking if the importer configuration asset's instance was created.
            bool isConfigurationAssetCreated = assetImportConfigurationAsset != null;

            // Check if the importer configuration instance and the asset save path exist.
            if (isConfigurationAssetCreated && configurationAssetSavePathExist)
            {
                // Getting the selected import configuration asset directory where the configuration asset is created.
                string configurationAssetCreationDirectory = AssetImportDirectory.GetAssetDirectory(importConfigurationAssetSavePath);

                // Creating a new scriptable object import configuration asset to the import configuration path.
                AssetDatabase.CreateAsset(assetImportConfigurationAsset, importConfigurationAssetSavePath);

                // Save data base assets.
                AssetDatabase.SaveAssets();
            }
            else
            {
                // Warning message to log in the unity debug console window.
                string warningLogMessage = "Asset importer toolkit : Configuration file creation operation cancelled/failed: Import configuration asset file not created.";

                // Logging a new warning message to the console when the configuration asset file creation is cancelled by the user.
                Debugger.LogWarning(className : classLogName, message : warningLogMessage);

                return;
            }
        }
    }
}