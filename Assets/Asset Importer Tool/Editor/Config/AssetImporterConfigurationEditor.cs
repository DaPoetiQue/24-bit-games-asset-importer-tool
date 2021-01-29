﻿// Libraries
using UnityEngine;
using UnityEditor;

// --Namespace
namespace AssetImporterToolkit
{
    // Asset importer configuration editor class.
    [CustomEditor(typeof(AssetImporterConfiguration))]
    public class AssetImporterConfigEditor : Editor
    {
        // Create a new scriptable object import configuration file.
        [MenuItem("24 Bit Games/Asset Importer/Create Configuration Asset")]
        private static void CreateImportConfiguration()
        {
            // Creating a new importer configuration instance.
            var importerConfiguration = CreateInstance<AssetImporterConfiguration>();

            // Getting the path to save newely created scriptable object import configuration asset.
            string ImportConfigurationPath =  EditorUtility.SaveFilePanelInProject("Asset Importer", "Asset Import Configuration", "asset", "");

            // Check if the importer configuration instance and the asset save path exist.
            if(importerConfiguration && !string.IsNullOrEmpty(ImportConfigurationPath))
            {
                // Getting a selected directory where the configuratio asset is created.
                string selectedDirectory = AssetImportDirectory.GetAssetDirectory(ImportConfigurationPath);

                // Checking if there sub directories inside the selected directory.
                string[] subDirectories = AssetDatabase.GetSubFolders(selectedDirectory);

                // Creating a new scriptable object import configuration asset to the import configuration path.
                AssetDatabase.CreateAsset(importerConfiguration, ImportConfigurationPath);

                // Assigning the scriptable object import configuration asset path to the newely created configuration asset file.
                importerConfiguration.AssetImporterConfigurationDirectory(AssetImportDirectory.GetAssetDirectory(ImportConfigurationPath));

                // --Checking if sub directories were found.
                if (subDirectories.Length > 0)
                {
                    // Looping through found sub directories
                    foreach (string directory in subDirectories)
                    {
                        // Adding the scriptable object import configuration asset path to the newely created configuration asset file.
                        importerConfiguration.AssetImporterConfigurationDirectory(directory);
                    }
                }

                // Log successs
                Debug.Log("A new import configuration asset file was successfully created at path : " + ImportConfigurationPath);
            }
            else
            {
                // Logging a new message to the console
                Debug.Log("Asset importer toolkit : Configuration file creation operation canceled/failed: Import configuration asset file not created");

                // Return from function
                return;
            }
        }

        // On inspector GUI method
        public override void OnInspectorGUI()
        {
            // Calling thebinspector gui base
            base.OnInspectorGUI();

            // Checking if the assets update 
            if (GUILayout.Button("Update Assets", GUILayout.Height(25)))
            {
                // Getting he currently selected configuration asset file in the project window
                AssetImporterConfiguration[] configurationAsset = Selection.GetFiltered<AssetImporterConfiguration>(SelectionMode.Assets);

                // Checking if has a configuration asset selected
                if (configurationAsset.Length > 0)
                {
                    // Updating imported assets retroactively using the currently selected configuration asset at the first index
                    OnImportedAssetsConfigurationUpdate(configurationAsset[0]);
                }
                else
                {
                    // Log a new error message
                    Debug.LogError("Configuration asset invalid/not found.");
                }
            }
        }

        // Used to update assets retroactively
        public void OnImportedAssetsConfigurationUpdate(AssetImporterConfiguration configurationAsset)
        {
            // Checking if a configuration asset exist
            if (configurationAsset)
            {
                // Checking if there are included asset directories for the current selected configuration asset
                if (configurationAsset.IncludedAssetDirectory.Count > 0)
                {
                    // Looping through included directories
                    for (int i = 0; i < configurationAsset.IncludedAssetDirectory.Count; i++)
                    {
                        // Reimport assets from the included folders
                        AssetsReimporter.OnReimportAssetsAtPath(configurationAsset.IncludedAssetDirectory[i]);
                    }
                }
                else
                {
                    // Log a new warning
                    Debug.LogWarning("There are currently no included asset directories to update.");

                    // Returning from this function
                    return;
                }
            }
        }
    }
}