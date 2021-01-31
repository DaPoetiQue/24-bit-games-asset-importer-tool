// Libraries
using System.IO;
using System.Linq;

// Namespace
namespace AssetImporterToolkit
{
    // Utilities class
    public static class Utilities
    {
        // Configuration asset search pattern
        public static string ConfigurationAssetSearchPattern()
        {
            // Create a new search pattern
            string searchPattern = "*" + AllowedFileExtension.ConfigurationAssetExtension;

            // Returning the new search pattern
            return searchPattern;
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
    }
}