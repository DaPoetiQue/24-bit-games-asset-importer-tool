// Libraries
using System.Collections.Generic;
using UnityEngine;

// Namespace
namespace AssetImporterToolkit
{
    // Asset imorter configuration scriptable object class.
    [CreateAssetMenu(fileName = "Asset Import Configuration", menuName = "24 Bit Games/Create Configuration Asset")]
    public class ConfigurationAsset : ScriptableObject
    {
        // Texture settings region.
        #region Texture settings

        // This is the texture import configuration.
        [Space(5)]
        public TextureImporterConfiguration TextureImportConfiguration = new TextureImporterConfiguration() { MaximumTextureSize = TextureSizeOption._1024, AnisotropicFilteringLevel = 0, m_TextureOveridePlatormOption = PlatformOption.Android };

        #endregion // Ending texture settings region.

        // Audio settings region.
        #region Audio settings

        // this is for audio import configuration.
        [Space(5)]
        public AudioImporterConfiguration AudioImportConfiguration = new AudioImporterConfiguration() { AudioOveridePlatormOption = PlatformOption.Android };

        #endregion // Ending audio settings region.

        // Included asset directory library variables
        #region Included asset directory library

        // This is a initialized list of included assets directory.
        [Space(5)]
        public AssetLibrary IncludedAssetDirectoryLibrary = new AssetLibrary() { AssetDirectoryList = new List<string>() };

        // Allow Debug
        [Space(5)]
        public bool AllowDebug;

        #endregion // Ending audio settings region.

        // Asset directories function region.
        #region Asset directories

        // This function adds configuration directory path to the library of included or affected directories.
        public void AddConfigurationAssetIncludedDirectoryToLibrary(string path)
        {
            // Checking if path is already included
            bool pathNotPreviouslyInclueded = !IncludedAssetDirectoryLibrary.AssetDirectoryList.Contains(path);

            // Check if path doesn't exist
            if (pathNotPreviouslyInclueded)
            {
                // Assigning the default path to the asset importer.
                IncludedAssetDirectoryLibrary.AssetDirectoryList.Add(path);
            }
        }

        // This function returns all the included assets directories.
        public AssetLibrary GetIncludedAssetsDirectoryLibrary()
        {
            // Returning AssetLibrary
            return IncludedAssetDirectoryLibrary;
        }

        // This function returns the amount of directories included to be affected by the configuration file.
        public int GetIncludedAssetsDirectoryLibraryCount()
        {
            // Returning the amount of included asset directories
            return IncludedAssetDirectoryLibrary.AssetDirectoryList.Count;
        }

        #endregion // Ended asset directories function region.
    }
}