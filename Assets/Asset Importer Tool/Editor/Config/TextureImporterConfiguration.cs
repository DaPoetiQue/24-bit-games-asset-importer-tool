// Libraries
using System;
using UnityEngine;

// Namespace
namespace AssetImporterToolkit
{
    // Texture configurations definition
    [Serializable]
    public struct TextureImporterConfiguration
    {
        // Texture settings region.
        #region Texture settings

        // Texture importer settings inspector header.
        [Space(3)]
        [Header("Texture Importer Settings")]

        // Maximum texture size option.
        [Space(3)]
        public TextureSizeOption MaximumTextureSize;

        // Anisotropic filtering level value.
        [Space(3)]
        [Range(0, 16)]
        public int AnisotropicFilteringLevel;

        // Overide texture settings for selected a runtime platform.
        [Space(3)]
        public PlatformOption m_TextureOveridePlatormOption;

        #endregion // Ending texture settings region.
    }
}