// --Libraries
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

// --Namespace
namespace AssetImporterToolkit
{
    [CanEditMultipleObjects]
    public class AssetImporter : AssetPostprocessor
    {
        // Asset Configuation Search Filter
        public static string m_ImportConfigAssetFindFilter = "asset, config, configuration, import";

        // Texture Configuration
        private AssetImporterConfigurations importConfiguration = null;

        // --On Pre-Proccess Model
        public void OnPreproccessModel()
        {
            // Get Import Configuration
            importConfiguration = GetAssetImportConfiguration(assetPath);

            // Check If Configuration Exist
            if (importConfiguration)
            {
                // --Log
                Debug.Log("Success, Audio Path : " + importConfiguration.m_IncludedAssetDirectory[0] + " Included For Configuration");
            }
            else
            {
                // --Log
                Debug.LogWarning("Path Not Included For Configuration");

                // --Return
                return;
            }
        }

        // --On Pre-Process Texture
        public void OnPreprocessTexture()
        {
            // --Get Import Configuration
            importConfiguration = GetAssetImportConfiguration(assetPath);

            // --Check If Configuration Exist
            if (importConfiguration)
            {
                // --Texture Impoprter
                var textureImporter = assetImporter as TextureImporter;

                // --Return
                if (textureImporter == null) return;

                // --Get Settings
                // textureImporter.maxTextureSize = 

                // --Overide Settings
                if (importConfiguration.m_TextureOveridePlatormOption != PlatformOption.None)
                {
                    // --Overiding Platform Settings
                    //textureImporter.SetTextureSettings()

                    // textureImporter.anisoLevel
                    // textureImporter.SetOverrideSampleSettings(importConfiguration.m_AudioOveridePlatormOption.ToString(), _Settings);


                    // --Log
                    Debug.Log("Overiding Platform Settings");
                }

                // --Log
                Debug.Log("Success, Texture Path : " + importConfiguration.m_IncludedAssetDirectory[0] + " Included For Configuration");
            }
            else
            {
                // --Log
                Debug.LogWarning("Texture Path Not Included For Configuration");

                // --Return
                return;
            }
        }

        // --On Preprocess Audio
        public void OnPreprocessAudio()
        {
            // --Get Import Configuration
            importConfiguration = GetAssetImportConfiguration(assetPath);

            // --Check If Configuration Exist
            if (importConfiguration)
            {
                // --Audio Impoprter
                var audioImporter = assetImporter as AudioImporter;

                // --Return
                if (audioImporter == null) return;

                // --Create A New Audio Configuration
                AudioImporterSampleSettings audioConfiguration = audioImporter.defaultSampleSettings;

                // --Add Settings
                audioConfiguration.loadType = importConfiguration.m_LoadType;

                audioConfiguration.sampleRateSetting = importConfiguration.m_SampleRate;

                audioConfiguration.compressionFormat = importConfiguration.m_CompressionFormat;

                audioConfiguration.quality = 0.01f;

                audioImporter.defaultSampleSettings = audioConfiguration;


                // --Overide Settings
                if (importConfiguration.m_AudioOveridePlatormOption != PlatformOption.None)
                {
                    // --Overiding Platform Settings
                    audioImporter.SetOverrideSampleSettings(importConfiguration.m_AudioOveridePlatormOption.ToString(), audioConfiguration);


                    // --Log
                    Debug.Log("Overiding Platform Settings");
                }
            }
            else
            {
                // --Log
                Debug.LogWarning("Audio Path Not Included For Configuration");

                // --Return
                return;
            }
        }

        // --On Post Process All Assets
        public void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssets)
        {

        }

        // --Get Asset Import Configuration
        private AssetImporterConfigurations GetAssetImportConfiguration(string path)
        {
            // --Check If Configuration File
            if (assetPath.StartsWith("Assets/") && !assetPath.EndsWith(".asset"))
            {
                // --Guids
                string[] guids = AssetImportDirectory.FindImportConfigurations(m_ImportConfigAssetFindFilter);

                // --Check If Guids Are Found
                if (guids.Length > 0)
                {
                    // --Get Path
                    string importConfigFilePath = AssetDatabase.GUIDToAssetPath(guids[0]);

                    // --Load Configuration
                    var importconfiguration = AssetDatabase.LoadAssetAtPath<AssetImporterConfigurations>(importConfigFilePath);

                    // --Check If Configuration File Loaded Successfully
                    if (importconfiguration && importconfiguration.m_IncludedAssetDirectory.Count > 0)
                    {
                        // --Asset Directory
                        string assetDirectory = AssetImportDirectory.GetAssetDirectory(path);

                        if (importconfiguration.m_IncludedAssetDirectory.Contains(assetDirectory))
                        {
                            // --Log
                            Debug.Log("Folder At Path : " + assetDirectory + " Is Included.");

                            // --Return Config
                            return importconfiguration;
                        }
                        else
                        {
                            // --Log Warning
                            Debug.LogWarning("Folder At Path : " + assetDirectory + " Is Not Included.");

                            // --Return Null
                            return null;
                        }
                    }
                    else
                    {
                        // --Log
                        Debug.LogWarning("Config Asset Not Found.");

                        // --Return Null
                        return null;
                    }
                }
                else
                {
                    // --Log
                    Debug.LogWarning("Config Asset Not Found.");

                    // --Return Null
                    return null;
                }
            }
            else
            {
                // --Log
                Debug.LogWarning("Not A Configuration File");

                // --Return Null
                return null;
            }
        }
    }
}
