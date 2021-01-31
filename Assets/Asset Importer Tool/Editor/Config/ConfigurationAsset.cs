using System.Collections.Generic;
using UnityEngine;

namespace AssetImporterToolkit
{
    [CreateAssetMenu(fileName = "Asset Import Configuration", menuName = "24 Bit Games/Create Configuration Asset")]
    public class ConfigurationAsset : ScriptableObject
    {
        #region Texture settings

        [Space(5)]
        public TextureImporterConfiguration TextureImportConfiguration = new TextureImporterConfiguration() { MaximumTextureSize = TextureSizeOption._1024, AnisotropicFilteringLevel = 0, m_TextureOveridePlatormOption = PlatformOption.Android };

        #endregion 

        #region Audio settings

        [Space(5)]
        public AudioImporterConfiguration AudioImportConfiguration = new AudioImporterConfiguration() { AudioOveridePlatormOption = PlatformOption.Android };

        #endregion

        #region Included asset directory library

        [Space(5)]
        public AssetLibrary IncludedAssetDirectoryLibrary = new AssetLibrary() { AssetDirectoryList = new List<string>() };

        [Space(5)]
        public bool AllowDebug;

        #endregion

        #region Asset directories

        public void AddConfigurationAssetIncludedDirectoryToLibrary(string path)
        {
            bool pathNotPreviouslyInclueded = !IncludedAssetDirectoryLibrary.AssetDirectoryList.Contains(path);

            if (pathNotPreviouslyInclueded)
            {
                IncludedAssetDirectoryLibrary.AssetDirectoryList.Add(path);
            }
        }

        public AssetLibrary GetIncludedAssetsDirectoryLibrary()
        {
            return IncludedAssetDirectoryLibrary;
        }

        public int GetIncludedAssetsDirectoryLibraryCount()
        {
            return IncludedAssetDirectoryLibrary.AssetDirectoryList.Count;
        }

        #endregion
    }
}