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
        // Importer configuration asset file.
        private static ConfigurationAsset importConfiguration = null;

        // This function is used to add configuration settings to imported texture assets.
        private void OnPreprocessTexture()
        {
            // Getting asset path directry in lower case type.
            string assetDirectory = AssetImportDirectory.GetAssetDirectory(assetPath.ToLower());

            // Getting import configuration asset file for the imported texture asset.
            importConfiguration = Configurations.GetAssetImportConfiguration(assetDirectory);

            // Post processing asset file using import settings.
            AssetImporterPostProcessor.ProcessTextureAsset(importConfiguration, this);
        }

        // This function is used to add configuration settings to imported audio assets.
        private void OnPreprocessAudio()
        {
            // Getting asset path directry in lower case type.
            string assetDirectory = AssetImportDirectory.GetAssetDirectory(assetPath.ToLower());

            // Getting import configuration asset file for the imported texture asset.
            importConfiguration = Configurations.GetAssetImportConfiguration(assetDirectory);

            // Post processing asset file using import settings.
            AssetImporterPostProcessor.ProcessAudioAsset(importConfiguration, this);
        }

        // This function is used to add configuration asset file's included or affected asset directories.
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            // Checking through the imported project assets. 
            foreach (string assetPath in importedAssets)
            {
                // Checcking if the current asset path contains the file with the given extension.
                bool assetHasRequiredExtension = assetPath.Contains(AllowedFileExtension.ConfigurationAssetExtension);

                // --Checking if the asset has the extension of the type given.
                if (assetHasRequiredExtension)
                {
                    // Getting import configuration asset file.
                    importConfiguration = Configurations.GetAssetImportConfiguration(assetPath);

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
