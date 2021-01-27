// Libraries
using UnityEngine;
using UnityEditor;

// --Namespace
namespace AssetImporterToolkit
{
    // Asset importer configuration editor class.
    public class AssetImporterConfigEditor : Editor
    {
        // Import configuration asset path.
        public static string ImportConfigurationPath;

        // Create a new scriptable object import configuration file.
        [MenuItem("24 Bit Games/Asset Importer/Create Configuration Asset")]
        private static void CreateImportConfiguration()
        {
            // Creating a new importer configuration instance.
            AssetImporterConfiguration importerConfiguration = CreateInstance<AssetImporterConfiguration>();

            // Getting the path to save newely created scriptable object import configuration asset.
            ImportConfigurationPath =  EditorUtility.SaveFilePanelInProject("Asset Importer", "Asset Import Configuration", "asset", "");

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
    }
}