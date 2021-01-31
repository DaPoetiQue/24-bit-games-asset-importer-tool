using System;
using UnityEditor;

namespace AssetImporterToolkit
{
    public static class AssetImporterPostProcessor
    {
        public static void ProcessTextureAsset(ConfigurationAsset importConfiguration, AssetPostprocessor assetPostprocessor)
        {
            if (AssetImportDirectory.IsValidConfigurationAsset(importConfiguration))
            {
                try
                {
                    string runtimePlatformName = importConfiguration.TextureImportConfiguration.m_TextureOveridePlatormOption.ToString();

                    var textureImporter = assetPostprocessor.assetImporter as TextureImporter;

                    int maximumTextureSize = (int)importConfiguration.TextureImportConfiguration.MaximumTextureSize;

                    textureImporter.maxTextureSize = maximumTextureSize;

                    textureImporter.anisoLevel = importConfiguration.TextureImportConfiguration.AnisotropicFilteringLevel;

                    var platformOverides = textureImporter.GetPlatformTextureSettings(runtimePlatformName);

                    platformOverides.overridden = importConfiguration.TextureImportConfiguration.m_TextureOveridePlatormOption != PlatformOption.None;

                    platformOverides.maxTextureSize = maximumTextureSize;

                    textureImporter.SetPlatformTextureSettings(platformOverides);
                }
                catch (Exception e)
                {
                    Debugger.ThrowException(e);
                }
            }
            else
            {

                if (importConfiguration.AllowDebug)
                {
                    Debugger.Log(className: "Asset Importer Post Processor", message: "This folder is not affected by the asset importer tool. add the folder path to the included directories path list of a configuration asset file.");
                }
            }
        }

        public static void ProcessAudioAsset(ConfigurationAsset importConfiguration, AssetPostprocessor assetPostprocessor)
        {
            if (AssetImportDirectory.IsValidConfigurationAsset(importConfiguration))
            {
                try
                {
                    var audioImporter = assetPostprocessor.assetImporter as AudioImporter;

                    AudioImporterSampleSettings audioConfiguration = audioImporter.defaultSampleSettings;

                    audioConfiguration.loadType = importConfiguration.AudioImportConfiguration.LoadType;
                    audioConfiguration.sampleRateSetting = importConfiguration.AudioImportConfiguration.SampleRate;
                    audioConfiguration.compressionFormat = importConfiguration.AudioImportConfiguration.CompressionFormat;
                    audioImporter.defaultSampleSettings = audioConfiguration;

                    bool platformOverideEnabled = importConfiguration.AudioImportConfiguration.AudioOveridePlatormOption != PlatformOption.None;

                    if (platformOverideEnabled)
                    {
                        audioImporter.SetOverrideSampleSettings(importConfiguration.AudioImportConfiguration.AudioOveridePlatormOption.ToString(), audioConfiguration);
                    }
                }
                catch (Exception e)
                {
                    Debugger.ThrowException(e);
                }
            }
            else
            {
                if(importConfiguration.AllowDebug)
                {
                    Debugger.Log(className: "Asset Importer Post Processor", message: "This folder is not affected by the asset importer tool. add the folder path to the included directories path list of a configuration asset file.");
                }
            }
        }
    }
}