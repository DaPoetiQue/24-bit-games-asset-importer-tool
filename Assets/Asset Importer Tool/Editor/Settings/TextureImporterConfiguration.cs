using System;
using UnityEngine;

namespace AssetImporterToolkit
{
    [Serializable]
    public struct TextureImporterConfiguration
    {
        #region Texture settings

        [Space(3)]
        [Header("Texture Importer Settings")]

        [Space(3)]
        public TextureSizeOption MaximumTextureSize;

        [Space(3)]
        [Range(0, 16)]
        public int AnisotropicFilteringLevel;

        [Space(3)]
        public PlatformOption m_TextureOveridePlatormOption;

        #endregion
    }
}