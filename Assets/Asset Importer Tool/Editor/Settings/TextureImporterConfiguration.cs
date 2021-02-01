// Used libraries.
using System;
using UnityEngine;

// Namespace.
namespace AssetImporterToolkit
{
    /// <summary>
    /// This struct contains the platform texture importer configurations.
    /// </summary>
    [Serializable]
    public struct TextureImporterConfiguration
    {
        // Texture settings region.
        #region Texture settings

        // Texture importer settings inspector header.
        [Space(5)]
        [Header("Texture Importer Settings")]

        // Maximum texture size option.
        [Space(5)]
        public TextureSizeOption MaximumTextureSize;

        // Anisotropic filtering level value.
        [Space(5)]
        [Range(0, 16)]
        public int AnisotropicFilteringLevel;

        // Overide texture settings for selected a runtime platform.
        [Space(5)]
        public PlatformOption m_TextureOveridePlatormOption;

        #endregion // Ending texture settings region.
    }
}