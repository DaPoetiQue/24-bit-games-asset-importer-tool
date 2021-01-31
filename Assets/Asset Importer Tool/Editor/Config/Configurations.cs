using System.IO;
using UnityEditor;

namespace AssetImporterToolkit
{
    public static class Configurations
    {
        private static string classLogName = "Configurations";

        public static ConfigurationAsset assetImporterConfiguration;

        public static void AddConfigurationIncludedAssetDirectory(ConfigurationAsset configurationAsset, string assetDirectory)
        {
            bool configurationHasNoIncludedAssetDirectories = configurationAsset.GetIncludedAssetsDirectoryLibraryCount() <= 0;

            if (AssetImportDirectory.IsValidConfigurationAsset(configurationAsset) && configurationHasNoIncludedAssetDirectories)
            {
                string newConfigurationAssetDirectory = AssetImportDirectory.GetAssetDirectory(assetDirectory);

                configurationAsset.AddConfigurationAssetIncludedDirectoryToLibrary(newConfigurationAssetDirectory);

                string[] subDirectoriesToInclude = AssetDatabase.GetSubFolders(newConfigurationAssetDirectory);

                bool subDirectoriesFound = subDirectoriesToInclude.Length > 0;

                if (subDirectoriesFound)
                {
                    foreach (string directory in subDirectoriesToInclude)
                    {
                        configurationAsset.AddConfigurationAssetIncludedDirectoryToLibrary(directory);
                    }
                }

                Debugger.Log(className : classLogName, message : "A new import configuration asset file was successfully created at path : " + newConfigurationAssetDirectory);
            }
        }

        public static ConfigurationAsset GetAssetImportConfiguration(string assetPath)
        {
            string[] assetImportConfigurationEntries = Directory.GetFiles(assetPath, Utilities.ConfigurationAssetSearchPattern(), SearchOption.TopDirectoryOnly);

            bool configurationAssetFilesFound = assetImportConfigurationEntries.Length > 0;

            if (configurationAssetFilesFound)
            {
                assetImporterConfiguration = AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(assetImportConfigurationEntries[0]);
            }
            else
            {
                string rootDirectory = Directory.GetParent(assetPath).ToString().ToLower();

                bool rootDirectoryExist = !string.IsNullOrEmpty(rootDirectory);

                if(rootDirectoryExist)
                {
                    string[] configurationAssetFileEntries = Directory.GetFiles(rootDirectory, Utilities.ConfigurationAssetSearchPattern(), SearchOption.TopDirectoryOnly);

                    bool configurationAssetFileEntriesFound = configurationAssetFileEntries.Length > 0;

                    if (configurationAssetFileEntriesFound)
                    {
                        assetImporterConfiguration = AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(configurationAssetFileEntries[0]);

                        if(assetImporterConfiguration.AllowDebug)
                        {
                            string logMessage = "Found : " + configurationAssetFileEntries.Length + " entries named : " + configurationAssetFileEntries[0] + " with : " + assetImporterConfiguration.GetIncludedAssetsDirectoryLibraryCount() + " included directories.";

                            Debugger.Log(className : classLogName, message : logMessage);
                        }
                    }
                }
                else
                {
                    if (assetImporterConfiguration.AllowDebug)
                    {
                        string logMessage = "This folder is not included for import configuration. create a new import configurtation asset file or add this directory to an existing configuration asset file.";

                        Debugger.LogWarning(className : classLogName, message : logMessage);
                    }
                }
            }

            return assetImporterConfiguration;
        }

        public static ConfigurationAsset GetConfigurationAssetFromDirectory(string assetDirectory)
        {
            return AssetDatabase.LoadAssetAtPath<ConfigurationAsset>(assetDirectory);
        }

        public static void OnUpdateIncludedAssetsUsingConfiguration(ConfigurationAsset configurationAsset)
        {
            if (AssetImportDirectory.IsValidConfigurationAsset(configurationAsset))
            {
                bool configurationIncludesDirectories = configurationAsset.GetIncludedAssetsDirectoryLibraryCount() > 0;

                if (configurationIncludesDirectories)
                {
                    AssetLibrary includedAssetDirectories = configurationAsset.GetIncludedAssetsDirectoryLibrary();

                    foreach (string directory in includedAssetDirectories.AssetDirectoryList)
                    {
                        AssetsReimporter.ReimportAssetsAtPath(directory, configurationAsset);
                    }

                    AssetDatabase.Refresh(ImportAssetOptions.Default);
                }
                else
                {
                    if(configurationAsset.AllowDebug)
                    {
                        Debugger.LogWarning(className : classLogName, message : "There are currently no included asset directories to update.");
                    }

                    return;
                }
            }
            else
            {
                if (configurationAsset.AllowDebug)
                {
                    string logMessage = "Configuration file missing.";

                    // Logging a new warning message to the console
                    Debugger.LogWarning(className : classLogName, message : logMessage);
                }

                return;
            }
        }
      
    }
}