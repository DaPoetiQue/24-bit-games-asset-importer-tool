// Used libraries.
using System.IO;
using System.Linq;

// Namespace.
namespace AssetImporterToolkit
{
    /// <summary>
    /// This class contains functions for configuration asset search pattern and getting multiple extension file entries functions.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// This function is used to get a formatted asset search pattern using extension .asset file. 
        /// </summary>
        /// <returns>This function returns a formatted search pattern string value.</returns>
        public static string GetAssetExtensionSearchPattern()
        {
            // Create a new search pattern
            string searchPattern = "*" + AllowedFileExtension.ConfigurationAssetExtension;

            // Returning the new search pattern results.
            return searchPattern;
        }

        /// <summary>
        /// This function is used to get files of a specific or multiple extensions from the a given directory.
        /// </summary>
        /// <param name="searchFolderPath">A folder or directory where the entries search will begin.</param>
        /// <returns>Returns a string array of files with the defined extension.</returns>
        public static string[] GetMultipleExtensionFileEntries(string searchFolderPath)
        {
            // Searching the assets directory for files using allowed extension filter type.
            var fileEntries = Directory.GetFiles(searchFolderPath).Where(file => AllowedFileExtension.FileExtensions.Any(file.ToLower().EndsWith));

            // Converting found file entries to an array of assets path.
            string[] fileEntriesToStringArray = fileEntries.ToArray();

            // Return all found asset files.
            return fileEntriesToStringArray;
        }
    }
}