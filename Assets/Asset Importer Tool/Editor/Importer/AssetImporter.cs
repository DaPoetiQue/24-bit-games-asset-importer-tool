using UnityEditor;

namespace AssetImporterToolkit
{
    [CanEditMultipleObjects]
    public class AssetImporter : AssetPostprocessor
    {
        public static ConfigurationAsset importConfiguration = null;

        public void OnPreprocessTexture()
        {
            string assetDirectory = AssetImportDirectory.GetAssetDirectory(assetPath.ToLower());

            importConfiguration = Configurations.GetAssetImportConfiguration(assetDirectory);

            AssetImporterPostProcessor.ProcessTextureAsset(importConfiguration, this);
        }

        public void OnPreprocessAudio()
        {
            string assetDirectory = AssetImportDirectory.GetAssetDirectory(assetPath.ToLower());

            importConfiguration = Configurations.GetAssetImportConfiguration(assetDirectory);

            AssetImporterPostProcessor.ProcessAudioAsset(importConfiguration, this);
        }

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string assetPath in importedAssets)
            {
                if (assetPath.Contains(AllowedFileExtension.ConfigurationAssetExtension))
                {
                    importConfiguration = Configurations.GetAssetImportConfiguration(assetPath);

                    if (importConfiguration)
                    {
                        Configurations.AddConfigurationIncludedAssetDirectory(importConfiguration, assetPath);
                    }
                }
            }
        }  
    }
}
