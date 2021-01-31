using System;
using UnityEngine;
using UnityEditor;

namespace AssetImporterToolkit
{
    [Serializable]
    public struct AudioImporterConfiguration
    {
        #region Audio settings

        [Space(3)]
        [Header("Audio Importer Settings")]

        [Space(3)]
        public AudioClipLoadType LoadType;

        [Space(3)]
        public AudioCompressionFormat CompressionFormat;

        [Space(3)]
        public AudioSampleRateSetting SampleRate;

        [Space(3)]
        public PlatformOption AudioOveridePlatormOption;

        #endregion
    }
}