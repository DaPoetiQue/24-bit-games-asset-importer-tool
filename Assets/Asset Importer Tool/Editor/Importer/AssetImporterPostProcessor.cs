// Used libraries.
using System;
using UnityEditor;

// Namespace.
namespace AssetImporterToolkit
{
    // Asset importer post processor class.
    public static class AssetImporterPostProcessor
    {
        /// <summary>
        /// This function is for post processing project imported texture assets.
        /// </summary>
        /// <param name="importConfiguration">Takes in the import configuration asset.</param>
        /// <param name="assetPostprocessor">Takes in the asset postprocessor class.</param>
        public static void ProcessTextureAsset(ConfigurationAsset importConfiguration, AssetPostprocessor assetPostprocessor)
        {
            // Trying to modify the texture importer with import configuration.
            try
            {
                // Converting texture overide platorm options to a string.
                string runtimePlatformName = importConfiguration.TextureImportConfiguration.m_TextureOveridePlatormOption.ToString();

                // Getting the texture impoprter settings from the imported asset.
                var textureImporter = assetPostprocessor.assetImporter as TextureImporter;

                // Getting the max size enum and converting it to a int value.
                int maximumTextureSize = (int)importConfiguration.TextureImportConfiguration.MaximumTextureSize;

                // Assigning texture importer platform settings maximum texture size.
                textureImporter.maxTextureSize = maximumTextureSize;

                // Assigning new settings.
                textureImporter.anisoLevel = importConfiguration.TextureImportConfiguration.AnisotropicFilteringLevel;

                // Getting current platform texture settings.
                var platformTextureSettings = textureImporter.GetPlatformTextureSettings(runtimePlatformName);

                // Checking if platform overide settings is enabled.
                platformTextureSettings.overridden = importConfiguration.TextureImportConfiguration.m_TextureOveridePlatormOption != PlatformOption.None;

                // Overiding setting for selected runtime platform.
                platformTextureSettings.maxTextureSize = maximumTextureSize;

                // Assigning final configured settings to the texture importer.
                textureImporter.SetPlatformTextureSettings(platformTextureSettings);
            }
            catch (Exception e)
            {
                // Throwing a new system exception.
                Debugger.ThrowException(e);
            }
        }

        /// <summary>
        /// This function is for post processing project imported audio assets.
        /// </summary>
        /// <param name="importConfiguration">Takes in the import configuration asset.</param>
        /// <param name="assetPostprocessor">Takes in the asset postprocessor class.</param>
        public static void ProcessAudioAsset(ConfigurationAsset importConfiguration, AssetPostprocessor assetPostprocessor)
        {
            // Trying to modify the audio importer settings.
            try
            {
                // Getting the audio asset impoprter settings
                var audioImporter = assetPostprocessor.assetImporter as AudioImporter;

                // Getting the  default audio importer sample settings from the new audio imported.
                AudioImporterSampleSettings audioConfiguration = audioImporter.defaultSampleSettings;

                // Applying the configured audio settings data to the imported audio asset.
                audioConfiguration.loadType = importConfiguration.AudioImportConfiguration.LoadType;
                audioConfiguration.sampleRateSetting = importConfiguration.AudioImportConfiguration.SampleRate;
                audioConfiguration.compressionFormat = importConfiguration.AudioImportConfiguration.CompressionFormat;

                // Assigning configured audio importer configurations.
                audioImporter.defaultSampleSettings = audioConfiguration;

                // Checking for active audio overide platorm option.
                bool platformOverideEnabled = importConfiguration.AudioImportConfiguration.AudioOveridePlatormOption != PlatformOption.None;

                // Checking if platform settings overide is enabled.
                if (platformOverideEnabled)
                {
                    // Overiding platform settings for selected runtime platform.
                    audioImporter.SetOverrideSampleSettings(importConfiguration.AudioImportConfiguration.AudioOveridePlatormOption.ToString(), audioConfiguration);
                }
            }
            catch (Exception e)
            {
                // Throwing a new system exception.
                Debugger.ThrowException(e);
            }
        }
    }
}