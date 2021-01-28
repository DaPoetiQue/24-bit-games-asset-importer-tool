// Libraries
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Configurations
    public static class Configurations
    {
        // A list of filter names for getting asset configuation files in the project.
        public static string m_ImportConfigAssetFindFilter = "asset, config, configuration, import";

        // --Asset Paths
        public static List<string> ImportConfigurationPathList = new List<string>();

        // This function returns a asset importer configuration file from a given path.
        public static AssetImporterConfiguration GetAssetImportConfiguration(string path)
        {
            // Checking if a configurable path exist.
            if (path.StartsWith("Assets/") && !path.EndsWith(".asset"))
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
                        if (importConfiguration && importConfiguration.IncludedAssetDirectory.Count > 0)
                        {
                            // Getting a asset directory from a path.
                            string assetDirectory = AssetImportDirectory.GetAssetDirectory(path);

                            // Checking if the asset directory is contained in the included asset directory.
                            if (importConfiguration.IncludedAssetDirectory.Contains(assetDirectory))
                            {
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

                        Debug.LogWarning("Asset importer configuration not found/created : This folder is not included in the importer configuration asset file. The imported asset will not be affected by any configuration.");
                        // Debug.LogWarning("A configuration asset is not found.");
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