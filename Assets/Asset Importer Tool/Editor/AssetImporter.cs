// Libraries.
using System.Collections.Generic;
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
        // A list of filter names for getting asset configuation files in the project.
        public static string m_ImportConfigAssetFindFilter = "asset, config, configuration, import";

        // Importer configuration asset file.
        AssetImporterConfiguration importConfiguration = null;

        // --Asset Paths
        public List<string> ImportConfigurationPathList = new List<string>();

        // On pre process texture assets.
        public void OnPreprocessTexture()
        {
            // Getting import configuration asset file for the imported texture asset.
            importConfiguration = GetAssetImportConfiguration(assetPath);

            // Checking if configuration asset file exists.
            if (importConfiguration)
            {
                // Gettting the texture impoprter settings from the imported asset.
                var textureImporter = assetImporter as TextureImporter;

                // Checking if the texture impoprter settings exist.
                if (textureImporter != null)
                {
                    // --Overide Settings
                    if (importConfiguration.m_TextureOveridePlatormOption != PlatformOption.None)
                    {
                        // Overiding platform settings
                        //textureImporter.SetTextureSettings()

                        // textureImporter.anisoLevel
                        // textureImporter.SetOverrideSampleSettings(importConfiguration.m_AudioOveridePlatormOption.ToString(), _Settings);

                        // --Log
                        Debug.Log("Overiding platform settings.");
                    }

                    // --Log
                    Debug.Log("Success, texture at path : " + importConfiguration.m_IncludedAssetDirectory[0] + " is included for configuration.");
                }
                else
                {
                    // Return from this function.
                    return;
                }
            }
            else
            {
                // Logging a warning
                Debug.LogWarning("Texture path not included for configuration.");

                // Return from this function.
                return;
            }
        }

        // On pre process audio assets
        public void OnPreprocessAudio()
        {
            // Getting import Configuration asset file for the imported audio asset.
            importConfiguration = GetAssetImportConfiguration(assetPath);

            // Checking if the import configuration asset file exist.
            if (importConfiguration)
            {
                // Getting the audio asset impoprter settings
                var audioImporter = assetImporter as AudioImporter;

                // Checking if the audio imported exist
                if (audioImporter != null)
                {
                    // Getting the  default audio importer sample settings from the new audio imported.
                    AudioImporterSampleSettings audioConfiguration = audioImporter.defaultSampleSettings;

                    // Applying the configured audio settings data to the imported audio asset.
                    audioConfiguration.loadType = importConfiguration.m_LoadType;
                    audioConfiguration.sampleRateSetting = importConfiguration.m_SampleRate;
                    audioConfiguration.compressionFormat = importConfiguration.m_CompressionFormat;
                    audioConfiguration.quality = 0.01f;
                    audioImporter.defaultSampleSettings = audioConfiguration;


                    // Checking if platform settings overide is enabled
                    if (importConfiguration.m_AudioOveridePlatormOption != PlatformOption.None)
                    {
                        // Overiding platform settings for selected runtime platform
                        audioImporter.SetOverrideSampleSettings(importConfiguration.m_AudioOveridePlatormOption.ToString(), audioConfiguration);

                        // Log
                        Debug.Log("Overiding platform settings");
                    }
                }    
                else
                {
                    // return from this function.
                    return;
                }
               
            }
            else
            {
                // --Log
                Debug.LogWarning("Audio path not included for configuration.");

                // --Return
                return;
            }
        }

        // --On post process all assets
        public void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssets)
        {

        }

        // This function returns a asset importer configuration file from a given path.
        private AssetImporterConfiguration GetAssetImportConfiguration(string path)
        {
            // Checking if a configurable path exist.
            if (assetPath.StartsWith("Assets/") && !assetPath.EndsWith(".asset"))
            {
                // Find guids fom import asset import directories using the import configuration asset find filter list.
                string[] guids = AssetImportDirectory.FindImportConfigurations(m_ImportConfigAssetFindFilter);

                // Checking if guids exist.
                if (guids.Length > 0)
                {
                    //Loop through loaded guids
                    foreach (string guid in guids)
                    {
                        // --Check If is asset file
                        if (AssetDatabase.GUIDToAssetPath(guid).Contains(".asset"))
                        {
                            // Add path to list
                            ImportConfigurationPathList.Add(AssetDatabase.GUIDToAssetPath(guid));

                            // Log config path
                            Debug.Log("Config path : " + AssetDatabase.GUIDToAssetPath(guid));
                        }
                    }

                    // --Check if import configuration path list is populated
                    if (ImportConfigurationPathList.Count > 0)
                    {
                        // Getting current import configuration file path
                        string importConfigurationFilePath = ImportConfigurationPathList[ImportConfigurationPathList.Count - 1];

                        // Loading a configuration settings asset file from the  import configuration file path.
                        AssetImporterConfiguration importConfiguration = AssetDatabase.LoadAssetAtPath<AssetImporterConfiguration>(importConfigurationFilePath);

                        // Checking if configuration file loaded successfully.
                        if (importConfiguration && importConfiguration.m_IncludedAssetDirectory.Count > 0)
                        {
                            // Getting a asset directory from a path.
                            string assetDirectory = AssetImportDirectory.GetAssetDirectory(path);

                            // Checking if the asset directory is contained in the included asset directory.
                            if (importConfiguration.m_IncludedAssetDirectory.Contains(assetDirectory))
                            {
                                // Log
                                Debug.Log("Folder at path : " + assetDirectory + " is included.");

                                // --Return a configuration.
                                return importConfiguration;
                            }
                            else
                            {
                                // Loggina a new warning.
                                Debug.LogWarning("Folder at path : " + assetDirectory + " is not included.");

                                // Returning null results.
                                return null;
                            }
                        }
                        else
                        {
                            // Returning null results.
                            return null;
                        }
                    }
                    else
                    {
                        // Log
                        Debug.LogWarning("A configuration asset is not found.");

                        // Returning null results.
                        return null;
                    }
                }
                else
                {
                    // Logging a new warning
                    Debug.LogWarning("A configuration asset is not found.");

                    // Returning null results.
                    return null;
                }
            }
            else
            {
                // Logging a new warning.
                Debug.LogWarning("Not a configuration file.");

                // Returning null results.
                return null;
            }
        }
    }
}
