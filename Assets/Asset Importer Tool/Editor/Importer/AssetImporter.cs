// Used libraries.
using UnityEditor;

// Namespace.
namespace AssetImporterToolkit
{
    // Allowing multiple objects to be edited.
    [CanEditMultipleObjects]

     // This function is used to add configuration settings to imported audio assets.
    public class AssetImporter : AssetPostprocessor
    {
        // This log name is used when logging messages to the debbuger.
        private static string classLogName = "Asset Importer";

        // This function is used to add configuration settings to imported texture assets.
        private void OnPreprocessTexture()
        {
            // Defining a new configuration asset.
            ConfigurationAsset textureImporterConfiguration;

            // Getting asset path directry in lower case type.
            string assetDirectory = AssetImportDirectory.GetAssetDirectory(assetImporter.assetPath.ToLower());

            // Getting import configuration asset file for the imported texture asset.
            textureImporterConfiguration = Configurations.GetAssetImportConfiguration(assetDirectory);

            // Validating the import configuration asset.
            bool configurationAssetIsValid = AssetImportDirectory.IsValidConfigurationAsset(textureImporterConfiguration);

            // Checking if import configurations asset file is valid.
            if (configurationAssetIsValid)
            {
                // Checking if the imported asset directories is included or affected by the configuration asset.
                if(textureImporterConfiguration.IncludedAssetDirectoryLibrary.Directories.Contains(assetDirectory))
                {
                    // Post processing asset file using import settings.
                    AssetImporterPostProcessor.ProcessTextureAsset(textureImporterConfiguration, this);

                    // Checking if the configuration asset allow debug is enabled.
                    if (textureImporterConfiguration.AllowDebug)
                    {
                        // This message that will be logged to the unity's debug console.
                        string logMessage = "The imported texture asset(s) has been configured successfully.";

                        // Logging a new message to the unity debug console.
                        Debugger.Log(className: classLogName, message: logMessage);
                    }
                }
            }
        }

        // This function is used to add configuration settings to imported audio assets.
        private void OnPreprocessAudio()
        {
            // Defining a new import configuration asset.
            ConfigurationAsset audioImportConfiguration ;

            // Getting asset path directry in lower case type.
            string assetDirectory = AssetImportDirectory.GetAssetDirectory(assetImporter.assetPath.ToLower());

            // Getting import configuration asset file for the imported texture asset.
            audioImportConfiguration = Configurations.GetAssetImportConfiguration(assetDirectory);

            // Validating the import configuration asset.
            bool configurationAssetIsValid = AssetImportDirectory.IsValidConfigurationAsset(audioImportConfiguration);

            // Checking if import configurations asset file is valid.
            if (configurationAssetIsValid)
            {
                // Checking if the imported asset directories is included or affected by the configuration asset.
                if (audioImportConfiguration.IncludedAssetDirectoryLibrary.Directories.Contains(assetDirectory))
                {
                    // Post processing asset file using import settings.
                    AssetImporterPostProcessor.ProcessAudioAsset(audioImportConfiguration, this);

                    // Checking if the configuration asset allow debug is enabled.
                    if (audioImportConfiguration.AllowDebug)
                    {
                        // This message that will be logged to the unity's debug console.
                        string logMessage = "The imported audio asset(s) has been configured successfully.";

                        // Logging a new message to the unity debug console.
                        Debugger.Log(className: classLogName, message: logMessage);
                    }
                }
            }
        }

        // This function is used to add configuration asset file's included or affected asset directories.
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            // Defining a new import configuration asset.
            ConfigurationAsset importConfiguration;

            // Checking through the imported project assets. 
            foreach (string assetPath in importedAssets)
            {
                // Checcking if the current asset path contains the file with the given extension.
                bool assetHasRequiredExtension = assetPath.Contains(AllowedFileExtension.ConfigurationAssetExtension);

                // --Checking if the asset has the extension of the type given.
                if (assetHasRequiredExtension)
                {
                    // Getting import configuration asset file.
                    importConfiguration = AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(assetPath);

                    // Checkig if asset importer configuration file has been loaded successfully.
                    if (importConfiguration)
                    {
                        // Initializing the newly created asset importer configuration file.
                        Configurations.AddConfigurationIncludedAssetDirectory(importConfiguration, assetPath);
                    }
                }
            }
        }
    }
}
