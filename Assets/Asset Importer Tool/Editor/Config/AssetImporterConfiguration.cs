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
        // A list of folder direcories that are included for pre processing.
        [Space(3)]
        public List<string> m_IncludedAssetDirectory = new List<string>();

        // Texture importer settings inspector header.
        [Space(3)]
        [Header("Texture Importer Settings")]

        [Space(3)]
        public Texture m_TextureImporterSettings;

        // Overide texture settings for selected a runtime platform.
        [Space(3)]
        public PlatformOption m_TextureOveridePlatormOption = PlatformOption.Android;

        // Audio importer settings inspector header.
        [Space(3)]
        [Header("Audio Importer Settings")]

        // Audio clip load option types.
        [Space(3)]
        public AudioClipLoadType m_LoadType;

        // Imported audio compression format options
        [Space(3)]
        public AudioCompressionFormat m_CompressionFormat;

        // Imported audio clip sample rate options
        [Space(3)]
        public AudioSampleRateSetting m_SampleRate;

        // Overide audio settings for a selected runtime platform.
        [Space(3)]
        public PlatformOption m_AudioOveridePlatormOption = PlatformOption.Android;

        // Setting default asset importer configuration directory.
        public void DefaultAssetImportConfigurationDirectory(string path)
        {
            // Assigning the default path to the asset importer 
            m_IncludedAssetDirectory.Add(path);
        }
    }
}