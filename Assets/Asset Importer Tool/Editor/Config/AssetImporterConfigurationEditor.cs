// Libraries
using System.IO;
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
            AssetImporterConfiguration importerConfiguration = CreateInstance<AssetImporterConfiguration>();

            // Getting the path to save newely created scriptable object import configuration asset.
            string importConfigurationPath =  EditorUtility.SaveFilePanelInProject("Asset Importer", "Asset Import Configuration", "asset", "");

            // Check if the importer configuration instance and the asset save path exist.
            if(importerConfiguration && !string.IsNullOrEmpty(importConfigurationPath))
            {
                // Getting a selected directory where the configuration asset is created.
                string selectedDirectory = AssetImportDirectory.GetAssetDirectory(importConfigurationPath);

                // Creating a new scriptable object import configuration asset to the import configuration path.
                AssetDatabase.CreateAsset(importerConfiguration, importConfigurationPath);

                // Initializing configuration asset
                Configurations.OnAssetImporterConfigurationAssetInitializer(importerConfiguration, selectedDirectory);
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
                    Configurations.OnImportedAssetsConfigurationUpdate(configurationAsset[0]);
                }
                else
                {
                    // Log a new error message
                    Debug.LogError("Configuration asset invalid/not found.");
                }
            }
        }
    }
}