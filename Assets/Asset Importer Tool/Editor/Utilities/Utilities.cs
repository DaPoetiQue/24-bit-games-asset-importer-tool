using System.IO;
using System.Linq;

namespace AssetImporterToolkit
{
    public static class Utilities
    {
        public static string ConfigurationAssetSearchPattern()
        {
            string searchPattern = "*" + AllowedFileExtension.ConfigurationAssetExtension;

            return searchPattern;
        }

        public static string[] GetMultipleExtensionFileEntries(string searchFolderPath)
        {
            var fileEntries = Directory.GetFiles(searchFolderPath).Where(file => AllowedFileExtension.FileExtensions.Any(file.ToLower().EndsWith));

            string[] fileEntriesToPathArray = fileEntries.ToArray();

            return fileEntriesToPathArray;
        }
    }
}