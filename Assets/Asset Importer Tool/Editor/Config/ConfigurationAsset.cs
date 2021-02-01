// Used libraries.
using System.Collections.Generic;
using UnityEngine;

// Namespace.
namespace AssetImporterToolkit
{
    // This function creates a new scriptable object asset that contains platform import settings.
    [CreateAssetMenu(fileName = "Asset Import Configuration", menuName = "24 Bit Games/Create Configuration Asset")]
    public class ConfigurationAsset : ScriptableObject
    {
        #region Texture settings section

        /// <summary>
        /// This are the settings for the texture import configuration.
        /// </summary>
        [Space(5)]
        public TextureImporterConfiguration TextureImportConfiguration = new TextureImporterConfiguration() { MaximumTextureSize = TextureSizeOption._1024, AnisotropicFilteringLevel = 0, m_TextureOveridePlatormOption = PlatformOption.Android };

        #endregion

        #region Audio settings section

        /// <summary>
        /// This are the settings for audio import configuration.
        /// </summary>
        [Space(5)]
        public AudioImporterConfiguration AudioImportConfiguration = new AudioImporterConfiguration() { AudioOveridePlatormOption = PlatformOption.Android };

        #endregion

        #region Asset path directory library section

        /// <summary>
        /// This is the initialized strings array list of included assets directory path that the configuration file will affect.
        /// </summary>
        [Space(5)]
        public AssetsDirectoryLibrary IncludedAssetDirectoryLibrary = new AssetsDirectoryLibrary() { Directories = new List<string>() };

        /// <summary>
        /// When enabled, the configuration asset file will log messages to the console.
        /// </summary>
        [Space(5)]
        public bool AllowDebug;

        #endregion

        #region Asset directories section

        /// <summary>
        /// Adds configuration directory path to the library of included or affected directories.
        /// </summary>
        /// <param name="assetDirectory">This function takes in a string parameter as a asset diretory that will be added tho the configuration asset file.</param>
        public void AddConfigurationAssetIncludedDirectoryToLibrary(string assetDirectory)
        {
            // This line checks if the asset path is already included with the configuration asset file.
            bool pathNotPreviouslyIncluded = !IncludedAssetDirectoryLibrary.Directories.Contains(assetDirectory);

            // Checking if path has not been included then add the path to the includede asset directory library list.
            if (pathNotPreviouslyIncluded)
            {
                // Converting the asset directory to a lower case.
                string lowerCasedAssetDirectory = assetDirectory.ToLower();

                // Adding the directory to the asset importer configuration asset file.
                IncludedAssetDirectoryLibrary.Directories.Add(lowerCasedAssetDirectory);
            }
        }

        /// <summary>
        /// This function returns all the included assets directories that are currenlty added to the configuration asset file.
        /// </summary>
        /// <returns>Returns a string list of all the included assets directories.</returns>
        public AssetsDirectoryLibrary GetIncludedAssetsDirectoryLibrary()
        {
            // Returning the asset library.
            return IncludedAssetDirectoryLibrary;
        }

        /// <summary>
        /// This function returns the amount of directories included to be affected by the configuration file.
        /// </summary>
        /// <returns>Returns the amount of directories.</returns>
        public int GetIncludedAssetsDirectoryLibraryCount()
        {
            // Returning the amount of included asset directories.
            return IncludedAssetDirectoryLibrary.Directories.Count;
        }

        #endregion
    }
}