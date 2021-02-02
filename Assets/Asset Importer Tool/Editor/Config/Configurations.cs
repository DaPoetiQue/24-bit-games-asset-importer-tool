// Used libraries.
using System.Collections.Generic;
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

                // Save asset data base.
                AssetDatabase.SaveAssets();

                // Refreshing the assets data base.
                AssetDatabase.Refresh();

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
            // Defining an assigned importer configuration asset.
            ConfigurationAsset importerConfigurationAsset;

            // Searching through the given asset directory to find configuration asset file entries.
            string[] assetImportConfigurationEntries = Directory.GetFiles(assetDirectory, Utilities.GetAssetExtensionSearchPattern(), SearchOption.TopDirectoryOnly);

            // Checking if the configuration entries were found.
            bool configurationAssetFilesFound = assetImportConfigurationEntries.Length > 0;

            // Checking if configuration asset file exist.
            if (configurationAssetFilesFound)
            {
                // Getting first asset found and aggigning it as a asset importer configuration asset.
                importerConfigurationAsset = AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(assetImportConfigurationEntries[0]);
            }
            else
            {
                // Trying to find a configuration asset in parent directory if it doesn't exist in current directory.
                importerConfigurationAsset = GetConfigurationAssetInParentDirectory(assetDirectory);
            }

            // Returning the requested asset import configuration asset file.
            return importerConfigurationAsset;
        }

        /// <summary>
        /// This function finds and return a configuration asset file in parent directory of the given path.
        /// </summary>
        /// <param name="assetDirectory">This function takes in a string parameter as a asset directory.</param>
        /// <returns>Returns a configuration asset file from the given asset directory.</returns>
        public static ConfigurationAsset GetConfigurationAssetInParentDirectory(string assetDirectory)
        {
            // Defining an assigned importer configuration asset.
            ConfigurationAsset importerConfigurationAsset;

            // Finding the root directory of the current file path and converting it to a lower cased string.
            string rootDirectory = Directory.GetParent(assetDirectory).ToString().ToLower();

            // Checking if the root directory was found.
            bool rootDirectoryExist = !string.IsNullOrEmpty(rootDirectory);

            // Checking if the root directory exist and search for configuration asset files.
            if (rootDirectoryExist)
            {
                // Searching for configuration asset types in the root directory.
                string[] configurationAssetFileEntries = Directory.GetFiles(rootDirectory, Utilities.GetAssetExtensionSearchPattern(), SearchOption.TopDirectoryOnly);

                // Checking if configuration the asset file entries were found in the root directory.
                bool configurationAssetFileEntriesFound = configurationAssetFileEntries.Length > 0;

                // Checking if configuration assets exist.
                if (configurationAssetFileEntriesFound)
                {
                    // Getting the first configuration asset file in the entries and assign it as the asset importer configuration asset file.
                    importerConfigurationAsset = AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(configurationAssetFileEntries[0]);
                }
                else
                {
                    // Getting the first configuration asset file in the entries and assign it as the asset importer configuration asset file.
                    importerConfigurationAsset = null;
                }
            }
            else
            {
                importerConfigurationAsset = null;
            }

            // Returning the requested asset import configuration asset file.
            return importerConfigurationAsset;
        }

        /// <summary>
        /// This function is for getting all project configuration asset files.
        /// </summary>
        /// <returns>This function returns a list of configuration asset files.</returns>
        public static List<string> GetAllConfigurationAssetFileDirectories()
        {
            //
            List<string> configurationAssetFileDirectories = new List<string>();

            // Getting all the project import configuration asset files.
            string[] projectImportConfigurationAssetFiles = Directory.GetFiles("Assets/", Utilities.GetAssetExtensionSearchPattern(), SearchOption.AllDirectories);

            // Checking if the import configuration ssset files exist in the project's assets directories.
            bool importConfigurationAssetFilesExist = projectImportConfigurationAssetFiles.Length > 0;

            // Checking if files exist.
            if (importConfigurationAssetFilesExist)
            {
                // Going through found project import configuration asset files.
                foreach (string assetFilePath in projectImportConfigurationAssetFiles)
                {
                    // Adding configuration asset files to the importer configuration asset list.
                    configurationAssetFileDirectories.Add(assetFilePath);
                }
            }
            else
            {
                // Warning message to log in unity debug console window
                string warningLogMessage = "There is no import configuration asset in this project. Create a new configuration asset file and try again.";

                // Log
                Debugger.LogWarning(className : classLogName, warningLogMessage);
            }

            return configurationAssetFileDirectories;
        }

        /// <summary>
        /// This function is used to determine the amount of import configuration asset files in the project.
        /// </summary>
        /// <returns>This function returns the amount of import configuration asset files in the project.</returns>
        public static int GetAllConfigurationAssetFilesCount()
        {
            // Returning all configuration asset files count.
            return GetAllConfigurationAssetFileDirectories().Count;
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
            // Checking if configuration file is valid
            bool isValidConfigurationAssetFile = AssetImportDirectory.IsValidConfigurationAsset(configurationAsset);

            // Checking if a configuration asset is not null.
            if (isValidConfigurationAssetFile)
            {
                // Checking if the configuration asset's importer assets library has included directories assigned.
                bool configurationIncludesDirectories = configurationAsset.GetIncludedAssetsDirectoryLibraryCount() > 0;

                // Checking if directories are included.
                if (configurationIncludesDirectories)
                {
                    // Getting a list of all included asset directories in the configuration asset file.
                    AssetsDirectoryLibrary includedAssetDirectoryLibrary = configurationAsset.GetIncludedAssetsDirectoryLibrary();

                    // Checking through all the found included directories assigned in the configuration asset file.
                    foreach (string directory in includedAssetDirectoryLibrary.Directories)
                    {
                        // Reimporting assets from the included directory.
                        AssetsReimporter.ReimportAssetsAtPath(directory, configurationAsset);
                    }
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