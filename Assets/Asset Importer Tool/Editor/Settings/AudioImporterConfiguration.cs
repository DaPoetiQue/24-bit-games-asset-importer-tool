// Used libraries.
using System;
using UnityEngine;
using UnityEditor;

// Namespace.
namespace AssetImporterToolkit
{
    /// <summary>
    /// This struct contains the platform audio importer configurations.
    /// </summary>
    [Serializable]
    public struct AudioImporterConfiguration
    {
        #region Audio settings

        // Audio importer settings inspector display header.
        [Space(5)]
        [Header("Audio Importer Settings")]

        // Audio clip load option types.
        [Space(5)]
        public AudioClipLoadType LoadType;

        // Imported audio compression format options.
        [Space(5)]
        public AudioCompressionFormat CompressionFormat;

        // Imported audio clip sample rate options.
        [Space(5)]
        public AudioSampleRateSetting SampleRate;

        // Options to overide audio settings for specific platforms.
        [Space(5)]
        public PlatformOption AudioOveridePlatormOption;

        #endregion
    }
}