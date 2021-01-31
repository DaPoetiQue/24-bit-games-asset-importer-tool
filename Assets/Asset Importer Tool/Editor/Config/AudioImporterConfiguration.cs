// Libraries
using System;
using UnityEngine;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Audio configurations definition struct
    [Serializable]
    public struct AudioImporterConfiguration
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