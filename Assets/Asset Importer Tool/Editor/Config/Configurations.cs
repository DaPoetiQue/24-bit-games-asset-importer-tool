// Used libraries.
using System.IO;
using UnityEditor;

// Namespace.
namespace AssetImporterToolkit
{
    /// <summary>
    /// This is the static configurations class for handling imported assets configurations.
    /// </summary>
    public static class Configurations
    {
        #region Variables

        // This log name is used when logging messages to the debbuger.
        private static string classLogName = "Configurations";

        // Declaring a new static asset importer configuration instance.
        private static ConfigurationAsset assetImporterConfiguration;

        #endregion

        #region functions

        /// <summary>
        /// Assigns asset directories to newly created configuration asset files.
        /// This static function also adds the project affected or included asset directories to a newly created configuration asset file.
        /// </summary>
        /// <param name="configurationAsset">Takes in the newly created configuration asset.</param>
        /// <param name="assetDirectory">Takes in the asset directory path.</param>
        public static void AddConfigurationIncludedAssetDirectory(ConfigurationAsset configurationAsset, string assetDirectory)
        {
            // Checking if asset importer configuration doesn't include any directories.
            bool configurationHasNoIncludedAssetDirectories = configurationAsset.GetIncludedAssetsDirectoryLibraryCount() <= 0;

            // Checking if asset importer configuration loaded successfully and that it doesn't include directories.
            if (AssetImportDirectory.IsValidConfigurationAsset(configurationAsset) && configurationHasNoIncludedAssetDirectories)
            {
                // Getting the directory of the newly created configuration asset.
                string newConfigurationAssetDirectory = AssetImportDirectory.GetAssetDirectory(assetDirectory);

                // Assigning the directory reference to the asset importer.
                configurationAsset.AddConfigurationAssetIncludedDirectoryToLibrary(newConfigurationAssetDirectory);

                // Checking for sub directories to include in the asset importer configuration directory.
                string[] subDirectoriesToInclude = AssetDatabase.GetSubFolders(newConfigurationAssetDirectory);

                // Checking if sun directories are found/initialized.
                bool subDirectoriesFound = subDirectoriesToInclude.Length > 0;

                // --Checking if sub directories were found in the asset directory.
                if (subDirectoriesFound)
                {
                    // Looping through found included sub directories to add to the asset importer configuration asset file.
                    foreach (string directory in subDirectoriesToInclude)
                    {
                        // Adding the scriptable object import configuration asset path to the newely created configuration asset file.
                        configurationAsset.AddConfigurationAssetIncludedDirectoryToLibrary(directory);
                    }
                }

                // Message to log in the unity debug console window.
                string logMessage = "A new import configuration asset file was successfully created at path : " + newConfigurationAssetDirectory;

                // Logging configuration asset creation successs message to the unity debugger console.
                Debugger.Log(className : classLogName, message : logMessage);
            }
        }

        /// <summary>
        /// Finds and load asset configuration from a give asset Directory.
        /// </summary>
        /// <param name="assetDirectory">This static function takes in a string parameter as the configuration asset's directory.</param>
        /// <returns>Returns a configuration asset file from the given asset directory.</returns>
        public static ConfigurationAsset GetAssetImportConfiguration(string assetDirectory)
        {
            // Searching through the given asset directory to find configuration asset file entries.
            string[] assetImportConfigurationEntries = Directory.GetFiles(assetDirectory, Utilities.GetAssetExtensionSearchPattern(), SearchOption.TopDirectoryOnly);

            // Checking if the configuration entries were found.
            bool configurationAssetFilesFound = assetImportConfigurationEntries.Length > 0;

            // Checking if configuration asset file exist.
            if (configurationAssetFilesFound)
            {
                // Getting first asset found and aggigning it as a asset importer configuration asset.
                assetImporterConfiguration = AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(assetImportConfigurationEntries[0]);
            }
            else
            {
                // Finding the root directory of the current file path and converting it to a lower cased string.
                string rootDirectory = Directory.GetParent(assetDirectory).ToString().ToLower();

                // Checking if the root directory was found.
                bool rootDirectoryExist = !string.IsNullOrEmpty(rootDirectory);

                // Checking if the root directory exist and search for configuration asset files.
                if(rootDirectoryExist)
                {
                    // Searching for configuration asset types in the root directory.
                    string[] configurationAssetFileEntries = Directory.GetFiles(rootDirectory, Utilities.GetAssetExtensionSearchPattern(), SearchOption.TopDirectoryOnly);

                    // Checking if configuration the asset file entries were found in the root directory.
                    bool configurationAssetFileEntriesFound = configurationAssetFileEntries.Length > 0;

                    // Checking if configuration assets exist.
                    if (configurationAssetFileEntriesFound)
                    {
                        // Getting the first configuration asset file in the entries and assign it as the asset importer configuration asset file.
                        assetImporterConfiguration = AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(configurationAssetFileEntries[0]);

                        // Checking if the asset importer configuration has debug enabled.
                        if(assetImporterConfiguration.AllowDebug)
                        {
                            // This message that will be logged to the console.
                            string logMessage = "Found : " + configurationAssetFileEntries.Length + " entries named : " + configurationAssetFileEntries[0] + " with : " + assetImporterConfiguration.GetIncludedAssetsDirectoryLibraryCount() + " included directories.";

                            // Logging a new message to the console. 
                            Debugger.Log(className : classLogName, message : logMessage);
                        }
                    }
                }
                else
                {
                    // Checking if the asset importer configuration has debug enabled for logging messages to the unity console window.
                    if (assetImporterConfiguration.AllowDebug)
                    {
                        // This message that will be logged to the console.
                        string logMessage = "This folder is not included for import configuration. create a new import configurtation asset file or add this directory to an existing configuration asset file.";

                        // Logging a new warning message to the unity console.
                        Debugger.LogWarning(className : classLogName, message : logMessage);
                    }
                }
            }

            // Returning the requested asset import configuration asset file.
            return assetImporterConfiguration;
        }

        /// <summary>
        /// This function finds and load a configuration asset from a give asset directory.
        /// </summary>
        /// <param name="assetDirectory">The function takes in a string parameter for the required asset directory</param>
        /// <returns>returns a configuration asset file</returns>
        public static ConfigurationAsset GetConfigurationAssetFromDirectory(string assetDirectory)
        {
            // Returning loaded configuration asset file from the asset directory.
            return AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(assetDirectory);
        }

        /// <summary>
        /// Updates all assets from the configuration asset's included diretories.
        /// </summary>
        /// <param name="configurationAsset">This function takes in a configuration asset file and use the data to find and update project affected assets.</param>
        public static void OnUpdateIncludedAssetsUsingConfiguration(ConfigurationAsset configurationAsset)
        {
            // Checking if a configuration asset is not null.
            if (AssetImportDirectory.IsValidConfigurationAsset(configurationAsset))
            {
                // Checking if the configuration asset's importer assets library has included directories assigned.
                bool configurationIncludesDirectories = configurationAsset.GetIncludedAssetsDirectoryLibraryCount() > 0;

                // Checking if directories are included.
                if (configurationIncludesDirectories)
                {
                    // Getting a list of all included asset directories in the configuration asset file.
                    AssetLibrary<string> includedAssetDirectories = configurationAsset.GetIncludedAssetsDirectoryLibrary();

                    // Checking through all the found included directories assigned in the configuration asset file.
                    foreach (string directory in includedAssetDirectories.AssetDirectoryList)
                    {
                        // Reimporting assets from the included directory.
                        AssetsReimporter.ReimportAssetsAtPath(directory, configurationAsset);
                    }

                    // Refreshing the asset database after re-importing the directory assets.
                    AssetDatabase.Refresh(ImportAssetOptions.Default);
                }
                else
                {
                    // Checking if the configuration asset allow debug is enabled to log to messages to the console.
                    if(configurationAsset.AllowDebug)
                    {
                        // This message that will be logged to the unity's debug console.
                        string logMessage = "There are currently no included asset directories to update.";

                        // Logging a new warning message to the unity debug console.
                        Debugger.LogWarning(className : classLogName, message : logMessage);
                    }

                    return;
                }
            }
            else
            {
                // Checking if the configuration asset allow debug is enabled.
                if (configurationAsset.AllowDebug)
                {
                    // This message that will be logged to the unity's debug console.
                    string logMessage = "Configuration file missing.";

                    // Logging a new warning message to the unity debug console.
                    Debugger.LogWarning(className : classLogName, message : logMessage);
                }

                return;
            }
        }

        #endregion
    }
}