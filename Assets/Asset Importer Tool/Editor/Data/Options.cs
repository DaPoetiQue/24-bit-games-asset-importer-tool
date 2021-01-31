// Libraries
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Namespace
namespace AssetImporterToolkit
{
    // Platform name options
    public enum PlatformOption
    {
        // A list of platform names
        None, Android, iOS, NintendoSwitch,
        PS4, PS5, Standalone, tvOS, WindowsStoreApps,
        WebGL, XBoxOne
    }

    // Texture Size Option
    public enum TextureSizeOption
    {
        // A list of texture sizes
        _32 = 32, _64 = 64, _128 = 128, _256 = 256,
        _512 = 512, _1024 = 1024, _2048 = 2048,
        _4096 = 4096, _8192 = 8192
    }

    // Directory library class
    public struct AssetData<T>
    {
        // A list of folder direcories that are included for pre processing.
        public List<T> data;
    }

    // Texture configurations definition
    [Serializable]
    public struct TextureImportConfigurations
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

    // Audio configurations definition struct
    [Serializable]
    public struct AudioImportConfigurations
    {
        // Audio settings region.
        #region Audio settings

        // Audio importer settings inspector header.
        [Space(3)]
        [Header("Audio Importer Settings")]

        // Audio clip load option types.
        [Space(3)]
        public AudioClipLoadType LoadType;

        // Imported audio compression format options
        [Space(3)]
        public AudioCompressionFormat CompressionFormat;

        // Imported audio clip sample rate options
        [Space(3)]
        public AudioSampleRateSetting SampleRate;

        // Overide audio settings for a selected runtime platform.
        [Space(3)]
        public PlatformOption AudioOveridePlatormOption;

        #endregion // Ended Audio settings region
    }

  


}