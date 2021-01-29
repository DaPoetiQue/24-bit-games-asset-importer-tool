﻿// Libraries.
using System;
using UnityEngine;
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
        AssetImporterConfiguration importConfiguration = null;

        // On pre process texture assets.
        public void OnPreprocessTexture()
        {
            // Log
            Debug.Log("Imported Texture");

            // Getting import configuration asset file for the imported texture asset.
            importConfiguration = Configurations.GetAssetImportConfiguration(assetPath);

            // --Checking if import configurations exist
            if(importConfiguration)
            {
                // Trying to modify the texture importer with import configuration.
                try
                {
                    // Converting texture overide platorm options to a string.
                    string runtimePlatformName = importConfiguration.m_TextureOveridePlatormOption.ToString();

                    // Getting the texture impoprter settings from the imported asset.
                    var textureImporter = assetImporter as TextureImporter;

                    // Getting the max size enum and converting it to a int value
                    int maximumTextureSize = (int)importConfiguration.MaximumTextureSize;

                    // Assigning texture importer platform settings maximum texture size.
                    textureImporter.maxTextureSize = maximumTextureSize;

                    // Assigning new settings.
                    textureImporter.anisoLevel = importConfiguration.AnisotropicFilteringLevel;

                    // Platform overides
                    var platformOverides = textureImporter.GetPlatformTextureSettings(runtimePlatformName);

                    // Overiding setting for selected runtime platform
                    platformOverides.overridden = importConfiguration.m_TextureOveridePlatormOption != PlatformOption.None;

                    // Assigning settings
                    textureImporter.SetPlatformTextureSettings(platformOverides);
                }
                catch (Exception e)
                {
                    // Throwing a system exception
                    throw e;
                }
            }
        }

        // Preprocessing imported audio assets
        public void OnPreprocessAudio()
        {
            // Log
            Debug.Log("Imported Audio");

            // Getting import Configuration asset file for the imported audio asset.
            importConfiguration = Configurations.GetAssetImportConfiguration(assetPath);

            // --Checking if import configurations exist
            if(importConfiguration)
            {
                // Trying to modify the audio importer settings.
                try
                {
                    // Getting the audio asset impoprter settings
                    var audioImporter = assetImporter as AudioImporter;

                    // Getting the  default audio importer sample settings from the new audio imported.
                    AudioImporterSampleSettings audioConfiguration = audioImporter.defaultSampleSettings;

                    // Applying the configured audio settings data to the imported audio asset.
                    audioConfiguration.loadType = importConfiguration.LoadType;
                    audioConfiguration.sampleRateSetting = importConfiguration.SampleRate;
                    audioConfiguration.compressionFormat = importConfiguration.CompressionFormat;

                    // Assigning configured audio importer configurations
                    audioImporter.defaultSampleSettings = audioConfiguration;

                    // Checking if platform settings overide is enabled
                    if (importConfiguration.AudioOveridePlatormOption != PlatformOption.None)
                    {
                        // Overiding platform settings for selected runtime platform
                        audioImporter.SetOverrideSampleSettings(importConfiguration.AudioOveridePlatormOption.ToString(), audioConfiguration);
                    }
                }
                catch (Exception e)
                {
                    // Throwing a new system exception
                    throw e;
                }
            }
            else
            {
                // Logging a new message
                Debug.Log("This folder is not affected by the asset importer tool. add the folder path to the included directories path list of a configuration asset file.");
            }
        }

        // On pre process assets
        public void OnPreprocessAsset()
        {
            // Checking if settings are missing
            if(assetImporter.importSettingsMissing)
                return;

            // Getting asset type
            Debug.Log("Imported asset of : " + assetImporter.name);

            // Get Asset
            // AudioImporter audioImporter = assetImporter as AudioImporter;

        }

        // --On post process all assets
        public void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssets)
        {
            // Log
            Debug.Log("Assets post processed");
        }
       
    }
}
