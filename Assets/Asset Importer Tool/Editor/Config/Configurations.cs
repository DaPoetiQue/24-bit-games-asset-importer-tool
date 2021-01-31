// Libraries
using System.IO;
using UnityEngine;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Configurations
    public static class Configurations
    {
        // Creating a new
        public static ConfigurationAsset assetImporterConfiguration;

        // This function assigns asset directories to newly created configuration asset files.
        public static void AddConfigurationIncludedAssetDirectory(ConfigurationAsset configurationAsset, string assetDirectory)
        {
            // Checking if asset importer configuration doesn't include any directories.
            bool configurationHasNoIncludedAssetDirectories = configurationAsset.GetIncludedAssetsDirectoryLibraryCount() <= 0;

            // Checking if asset importer configuration loaded successfully and that it doesn't include directories.
            if (AssetImportDirectory.IsValidConfigurationAsset(configurationAsset) && configurationHasNoIncludedAssetDirectories)
            {
                // Getting the directory of the newly created configuration asset
                string newConfigurationAssetDirectory = AssetImportDirectory.GetAssetDirectory(assetDirectory);

                // Assigning the directory reference to the asset importer.
                configurationAsset.AddConfigurationAssetIncludedDirectoryToLibrary(newConfigurationAssetDirectory);

                // Checking for sub directories to include in the asset importer configuration directory.
                string[] subDirectoriesToInclude = AssetDatabase.GetSubFolders(newConfigurationAssetDirectory);

                // Checking if sun directories are found/initialized.
                bool subDirectoriesFound = subDirectoriesToInclude.Length > 0;

                // --Checking if sub directories were found.
                if (subDirectoriesFound)
                {
                    // Looping through found included sub directories to add to the asset importer configuration asset file.
                    foreach (string directory in subDirectoriesToInclude)
                    {
                        // Adding the scriptable object import configuration asset path to the newely created configuration asset file.
                        configurationAsset.AddConfigurationAssetIncludedDirectoryToLibrary(directory);
                    }
                }

                // Log successs
                Debug.Log("A new import configuration asset file was successfully created at path : " + newConfigurationAssetDirectory);
            }
        }

        // This functions is used to get and loaded asset configuration
        public static ConfigurationAsset GetAssetImportConfiguration(string assetPath)
        {
            // Searching through the given asset path directory to find configuration asset file entries.
            string[] assetImportConfigurationEntries = Directory.GetFiles(assetPath, Utilities.ConfigurationAssetSearchPattern(), SearchOption.TopDirectoryOnly);

            // Checking if the configuration entries were found
            bool configurationAssetFilesFound = assetImportConfigurationEntries.Length > 0;

            // Checking if configuration asset file exist
            if (configurationAssetFilesFound)
            {
                // Getting first asset found and aggigning it as a asset importer configuration asset.
                assetImporterConfiguration = AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(assetImportConfigurationEntries[0]);
            }
            else
            {
                // Finding the root directory of the current file path and converting it to a lower cased string.
                string rootDirectory = Directory.GetParent(assetPath).ToString().ToLower();

                // Checking if the root directory was found
                bool rootDirectoryExist = !string.IsNullOrEmpty(rootDirectory);

                // Checking if the root directory exist and search for configuration asset files
                if(rootDirectoryExist)
                {
                    // Searching for configuration asset types in the root directory
                    string[] configurationAssetFileEntries = Directory.GetFiles(rootDirectory, Utilities.ConfigurationAssetSearchPattern(), SearchOption.TopDirectoryOnly);

                    // Checking if configuration the asset file entries were found in the root directory
                    bool configurationAssetFileEntriesFound = configurationAssetFileEntries.Length > 0;

                    // Checking if configuration assets exist
                    if (configurationAssetFileEntriesFound)
                    {
                        // Getting the first configuration asset file in the entries and assign it as the asset importer configuration asset file.
                        assetImporterConfiguration = AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(configurationAssetFileEntries[0]);

                        // Checking if the asset importer configuration has debug enabled
                        if(assetImporterConfiguration.AllowDebug)
                        {
                            // Logging a new message to the console. 
                            Debug.Log("Found : " + configurationAssetFileEntries.Length + " entries named : " + configurationAssetFileEntries[0] + " with : " + assetImporterConfiguration.GetIncludedAssetsDirectoryLibraryCount() + " included directories.");
                        }
                    }
                }
                else
                {
                    // Checking if the asset importer configuration has debug enabled
                    if (assetImporterConfiguration.AllowDebug)
                    {

                        // Logging a new warning message
                        Debug.LogWarning("This folder is not included for import configuration. create a new import configurtation asset file or add this directory to an existing configuration asset file.");
                    }
                }
            }

            // Getting files in directory
            return assetImporterConfiguration;
        }

        // This function find and load a configuration asset
        public static ConfigurationAsset GetConfigurationAssetFromDirectory(string assetDirectory)
        {
            // Returning loaded configuration asset
            return AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(assetDirectory);
        }

        // This function updates all assets from the configuration asset's included diretories.
        public static void OnUpdateIncludedAssetsUsingConfiguration(ConfigurationAsset configurationAsset)
        {
            // Checking if a configuration asset is not null
            if (AssetImportDirectory.IsValidConfigurationAsset(configurationAsset))
            {
                // Checking if the configuration asset's importer assets library has included directories assigned
                bool configurationIncludesDirectories = configurationAsset.GetIncludedAssetsDirectoryLibraryCount() > 0;

                // Checking if directories included
                if (configurationIncludesDirectories)
                {
                    // Getting all included asset directories
                    AssetLibrary includedAssetDirectories = configurationAsset.GetIncludedAssetsDirectoryLibrary();

                    // Loop through found inc
                    foreach (string directory in includedAssetDirectories.AssetDirectoryList)
                    {
                        // Reimport assets from the included directory
                        AssetsReimporter.OnReimportAssetsAtPath(directory, configurationAsset);
                    }

                    // Refresh asset database
                    AssetDatabase.Refresh(ImportAssetOptions.Default);
                }
                else
                {
                    // Checking if the configuration asset allow debug is enabled
                    if(configurationAsset.AllowDebug)
                    {
                        // Logging a new warning message
                        Debug.LogWarning("There are currently no included asset directories to update.");
                    }

                    // Returning from this function
                    return;
                }
            }
            else
            {
                // Checking if the configuration asset allow debug is enabled
                if (configurationAsset.AllowDebug)
                {
                    // Logging a new warning message to the console
                    Debug.LogWarning("Configuration file missing.");
                }

                // Returning from this function
                return;
            }
        }
      
    }
}