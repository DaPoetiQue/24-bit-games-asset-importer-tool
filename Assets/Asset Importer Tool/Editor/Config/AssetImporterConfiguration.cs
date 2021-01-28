// Libraries
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Asset imorter configuration scriptable object class.
    public class AssetImporterConfiguration : ScriptableObject
    {
        // Texture settings region.
        #region Texture settings

        // Texture importer settings inspector header.
        [Space(3)]
        [Header("Texture Importer Settings")]

        // Maximum texture size option.
        [Space(3)]
        public TextureSizeOption MaximumTextureSize = TextureSizeOption._1024;

        // Anisotropic filtering level value.
        [Space(3)]
        [Range(0, 16)]
        public int AnisotropicFilteringLevel;

        // Overide texture settings for selected a runtime platform.
        [Space(3)]
        public PlatformOption m_TextureOveridePlatormOption;

        #endregion // Ending texture settings region.

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

        // Asset directories function region.
        #region Asset directories

        // Directories importer settings header.
        [Space(3)]
        [Header("Importer Configuration Directories")]

        // A list of folder direcories that are included for pre processing.
        [Space(3)]
        public List<string> IncludedAssetDirectory = new List<string>();

        // Setting default asset importer configuration directory.
        public void AssetImporterConfigurationDirectory(string path)
        {
            // Check if path doesn't exist
            if(!IncludedAssetDirectory.Contains(path))
            {
                // Assigning the default path to the asset importer.
                IncludedAssetDirectory.Add(path);
            }
        }

        #endregion // Ended asset directories function region.
    }
}