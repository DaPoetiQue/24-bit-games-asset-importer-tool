﻿// Libraries
using System.IO;
using System.Linq;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Asset import directories class
    public static class AssetImportDirectory
    {
        // Getting a asset directory from a given path
        public static string GetAssetDirectory(string path) => Path.GetDirectoryName(path);

        // Finding existing import configuration asset using filters 
        public static string[] FindImportConfigurations(string filter) => AssetDatabase.FindAssets(filter);

        // Getting asset path from asset guid
        public static string GetAssetPathFromGuid(string assetGuid)
        {
            // Return
            return AssetDatabase.GUIDToAssetPath(assetGuid);
        }

        // Getting File Entries
        public static string[] GetMultipleExtensionFileEntries(string searchFolderPath)
        {
            // Searching the assets directory for files with supported extension
            var fileEntries = Directory.GetFiles(searchFolderPath).Where(file => AllowedFileExtension.FileExtensions.Any(file.ToLower().EndsWith));

            // Converting file entries to assets path
            string[] fileEntriesToPathArray = fileEntries.ToArray();

            // Return all found files
            return fileEntriesToPathArray;
        }

        // Getting the newly created configuration asset path
        public static string GetNewCreatedConfigurationAssetPath(string assetDirectory)
        {
            // Getting files at the given asset directory
            string[] configurationAssetEntries = Directory.GetFiles(assetDirectory, ConfigurationAssetSearchPattern(), SearchOption.AllDirectories);

            // Checking is assets found
            if(configurationAssetEntries.Length > 0)
            {
                // Returning the last array index value
                return configurationAssetEntries[configurationAssetEntries.Length - 1];
            }
            else
            {
                // Returning null
                return null;
            }
        }

        // This method id for checking if a configuration asset exist inside a given directory. 
        public static bool DirectoryContainsConfigurationAsset(string assetDirectory)
        {
            // Checking if tru
            if (Configurations.GetAssetImportConfiguration(assetDirectory))
            {
                // Return true now that the asset exist
                return true;
            }
            else
            {
                // Return false
                return false;
            }
        }

        // Configuration asset search pattern
        public static string ConfigurationAssetSearchPattern()
        {
            // Create a new search pattern
            string searchPattern = "*" + AllowedFileExtension.ConfigurationAssetExtension;

            // Returning the new search pattern
            return searchPattern;
        }
    }
}