// Libraries.
using UnityEditor;

// Namespace.
namespace AssetImporterToolkit
{
    // Allows multiple objects to be edited.
    [CanEditMultipleObjects]

    // Asset importer class for asset post proccessing.
    public class AssetImporter : AssetPostprocessor
    {
        // Importer configuration asset file.
        public static ConfigurationAsset importConfiguration = null;

        // On pre process texture assets.
        public void OnPreprocessTexture()
        {
            // Getting asset path directry in lower case type
            string assetDirectory = AssetImportDirectory.GetAssetDirectory(assetPath.ToLower());

            // Getting import configuration asset file for the imported texture asset.
            importConfiguration = Configurations.GetAssetImportConfiguration(assetDirectory);

            // Post processing asset file using import settings
            AssetImporterPostProcessor.ProcessTextureAsset(importConfiguration, this);
        }

        // Preprocessing imported audio assets
        public void OnPreprocessAudio()
        {
            // Getting asset path directry in lower case type
            string assetDirectory = AssetImportDirectory.GetAssetDirectory(assetPath.ToLower());

            // Getting import configuration asset file for the imported texture asset.
            importConfiguration = Configurations.GetAssetImportConfiguration(assetDirectory);

            // Post processing asset file using import settings
            AssetImporterPostProcessor.ProcessAudioAsset(importConfiguration, this);
        }

        // On post process all project assets
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            // Looping through imported assets
            foreach (string assetPath in importedAssets)
            {
                // --Check if asset type
                if (assetPath.Contains(AllowedFileExtension.ConfigurationAssetExtension))
                {
                    // Getting import configuration asset file
                    importConfiguration = Configurations.GetAssetImportConfiguration(assetPath);

                    // Check if asset importer configuration file is loaded successfully
                    if (importConfiguration)
                    {
                        // Initializing the newly created asset importer configuration fil
                        Configurations.AddConfigurationIncludedAssetDirectory(importConfiguration, assetPath);
                    }
                }
            }
        }  
    }
}
