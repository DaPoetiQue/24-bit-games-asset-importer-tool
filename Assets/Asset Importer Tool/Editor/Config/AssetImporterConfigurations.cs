// Libraries
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Asset importer configurations scriptable object class
    public class AssetImporterConfigurations : ScriptableObject
    {

        // Included asset directories
        [Space(3)]
        public List<string> m_IncludedAssetDirectory = new List<string>();

        // Texture header
        [Space(3)]
        [Header("Texture Importer Settings")]

        // Overide texture settings for android
        [Space(3)]
        public PlatformOption m_TextureOveridePlatormOption = PlatformOption.Android;

        // Header
        [Space(3)]
        [Header("Audio Importer Settings")]

        // Clip load type
        [Space(3)]
        public AudioClipLoadType m_LoadType;

        // Compression format
        [Space(3)]
        public AudioCompressionFormat m_CompressionFormat;

        // Sample rate
        [Space(3)]
        public AudioSampleRateSetting m_SampleRate;

        // Overide audio settings for android
        [Space(3)]
        public PlatformOption m_AudioOveridePlatormOption = PlatformOption.Android;

        // Default asset import configuration directory
        public void DefaultAssetImportConfigurationDirectory(string path)
        {
            // Assign new path
            m_IncludedAssetDirectory.Add(path);
        }
    }
}