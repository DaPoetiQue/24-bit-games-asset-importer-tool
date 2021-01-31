// Libraries
using System;
using System.Collections.Generic;
using UnityEngine;

// Namespace
namespace AssetImporterToolkit
{
    // This is the asset library for storing asset data
    [Serializable]
    public struct AssetLibrary
    {
        // Header
        [Header("Asset Library")]

        // This is a list for storing asset directory
        [Space(3)]
        public List<string> AssetDirectoryList;
    }
}