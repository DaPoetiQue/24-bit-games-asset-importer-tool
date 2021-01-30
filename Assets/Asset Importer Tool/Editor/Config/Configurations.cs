// Libraries
using System.IO;
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

        // Creating a new
        public static AssetImporterConfiguration assetImporterConfiguration;

        // On asset importer configuration asset initializer
        public static void OnAssetImporterConfigurationAssetInitializer(AssetImporterConfiguration configurationAsset, string assetPath)
        {
            // Checking if asset importer configuration loaded successfully and that it doesn't include any directories.
            if (configurationAsset && configurationAsset.IncludedAssetDirectory.Count <= 0)
            {
                // Getting the directory of the newly created configuration asset
                string assetDirectory = AssetImportDirectory.GetAssetDirectory(assetPath);

                // Assigning the directory reference to the asset importer.
                configurationAsset.AssetImporterConfigurationDirectory(assetDirectory);

                // Checking for sub directories to include in the asset importer configuration directory.
                string[] subDirectories = AssetDatabase.GetSubFolders(assetDirectory);

                // --Checking if sub directories were found.
                if (subDirectories.Length > 0)
                {
                    // Looping through found sub directories to add to the asset importer configuration asset file.
                    foreach (string directory in subDirectories)
                    {
                        // Adding the scriptable object import configuration asset path to the newely created configuration asset file.
                        configurationAsset.AssetImporterConfigurationDirectory(directory);
                    }
                }

                // Log successs
                Debug.Log("A new import configuration asset file was successfully created at path : " + assetDirectory);
            }
        }

        // Getting configuration file
        public static AssetImporterConfiguration GetAssetImportConfiguration(string assetPath)
        {
            // Checking
            string[] entry = Directory.GetFiles(assetPath, AssetImportDirectory.ConfigurationAssetSearchPattern(), SearchOption.TopDirectoryOnly);

            // Checking
            if(entry.Length > 0)
            {
                // Getting first entry
                assetImporterConfiguration = AssetDatabase.LoadAssetAtPath<AssetImporterConfiguration>(entry[0]);
            }
            else
            {
                // Getting the parent folder
                string parentFolderPath = Directory.GetParent(assetPath).ToString();

                // Getting asset files
                string[] assetFileEntries = Directory.GetFiles(parentFolderPath, AssetImportDirectory.ConfigurationAssetSearchPattern(), SearchOption.TopDirectoryOnly);

                //
                if(assetFileEntries.Length > 0)
                {
                    // Getting first entry
                    assetImporterConfiguration = AssetDatabase.LoadAssetAtPath<AssetImporterConfiguration>(assetFileEntries[0]);

                    // Log
                    Debug.Log("Found : " + assetFileEntries.Length + " entries named : " + assetFileEntries[0] + " with : " + assetImporterConfiguration.IncludedAssetDirectory.Count + " included directories.");
                }
                else
                {
                    Debug.Log("Parent not found. :(");
                }
            }

            // Getting files in directory
            return assetImporterConfiguration;
        }

        // Getting configuration asset from a given directory
        public static AssetImporterConfiguration GetConfigurationAssetFromDirectory(string assetDirectory)
        {
            // Returning loaded configuration asset
            return AssetDatabase.LoadAssetAtPath<AssetImporterConfiguration>(assetDirectory);
        }

        // Used to update assets retroactively
        public static void OnImportedAssetsConfigurationUpdate(AssetImporterConfiguration configurationAsset)
        {
            // Checking if a configuration asset exist
            if (configurationAsset)
            {
                // Checking if there are included asset directories for the current selected configuration asset
                if (configurationAsset.IncludedAssetDirectory.Count > 0)
                {
                    // Refresh asset database
                    AssetDatabase.Refresh(ImportAssetOptions.Default);

                    // Looping through included directories
                    for (int i = 0; i < configurationAsset.IncludedAssetDirectory.Count; i++)
                    {
                        // Reimport assets from the included folders
                        AssetsReimporter.OnReimportAssetsAtPath(configurationAsset.IncludedAssetDirectory[i]);
                    }

                    // Refresh asset database
                    AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
                }
                else
                {
                    // Logging a new warning message
                    Debug.LogWarning("There are currently no included asset directories to update.");

                    // Returning from this function
                    return;
                }
            }
        }

        //// Configuration asset search pattern
        //public static string ConfigurationAssetSearchPattern()
        //{
        //    // Create a new search pattern
        //    string searchPattern = "*" + AllowedFileExtension.ConfigurationAssetExtension;

        //    // Returning the new search pattern
        //    return searchPattern;
        //}

        // This function returns a asset importer configuration file from a given path.
        //public static AssetImporterConfiguration GetAssetImportConfiguration(string path)
        //{
        //    // Checking if a configurable path exist.
        //    if (path.StartsWith("Assets/") && !path.EndsWith(".asset"))
        //    {
        //        // Find guids fom import asset import directories using the import configuration asset find filter list.
        //        string[] guids = AssetImportDirectory.FindImportConfigurations(m_ImportConfigAssetFindFilter);

        //        // Checking if guids exist.
        //        if (guids.Length > 0)
        //        {
        //            //Loop through loaded guids
        //            foreach (string guid in guids)
        //            {
        //                // --Check If is asset file
        //                if (AssetDatabase.GUIDToAssetPath(guid).Contains(".asset"))
        //                {
        //                    // Add path to list
        //                    ImportConfigurationPathList.Add(AssetDatabase.GUIDToAssetPath(guid));
        //                }
        //            }

        //            // --Check if import configuration path list is populated
        //            if (ImportConfigurationPathList.Count > 0)
        //            {
        //                foreach (var item in ImportConfigurationPathList)
        //                {
        //                    // Log
        //                    Debug.Log("Configuration path found : " + item);
        //                }


        //                // Getting current import configuration file path
        //                string importConfigurationFilePath = ImportConfigurationPathList[ImportConfigurationPathList.Count - 1];

        //                // Loading a configuration settings asset file from the  import configuration file path.
        //                AssetImporterConfiguration importConfiguration = AssetDatabase.LoadAssetAtPath<AssetImporterConfiguration>(importConfigurationFilePath);

        //                // Checking if configuration file loaded successfully.
        //                if (importConfiguration && importConfiguration.IncludedAssetDirectory.Count > 0)
        //                {
        //                    // Getting a asset directory from a path.
        //                    string assetDirectory = AssetImportDirectory.GetAssetDirectory(path);

        //                    // Checking if the asset directory is contained in the included asset directory.
        //                    if (importConfiguration.IncludedAssetDirectory.Contains(assetDirectory))
        //                    {
        //                        // --Return a configuration.
        //                        return importConfiguration;
        //                    }
        //                    else
        //                    {
        //                        // Loggina a new warning.
        //                        Debug.LogWarning("Folder at path : " + assetDirectory + " is not included.");

        //                        // Returning null results.
        //                        return null;
        //                    }
        //                }
        //                else
        //                {
        //                    // Returning null results.
        //                    return null;
        //                }
        //            }
        //            else
        //            {
        //                // Log

        //                Debug.LogWarning("Asset importer configuration not found/created : This folder is not included in the importer configuration asset file. The imported asset will not be affected by any configuration.");
        //                // Debug.LogWarning("A configuration asset is not found.");
        //                // Returning null results.
        //                return null;
        //            }
        //        }
        //        else
        //        {
        //            // Logging a new warning
        //            Debug.LogWarning("A configuration asset is not found.");

        //            // Returning null results.
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        // Logging a new warning.
        //        Debug.LogWarning("Not a configuration file.");

        //        // Returning null results.
        //        return null;
        //    }
        //}
    }
}