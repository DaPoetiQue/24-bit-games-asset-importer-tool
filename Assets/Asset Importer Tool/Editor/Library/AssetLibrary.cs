// Used libraries.
using System;
using System.Collections.Generic;
using UnityEngine;

// Namespace.
namespace AssetImporterToolkit
{
    /// <summary>
    /// This is the asset library for storing asset related data.
    /// </summary>
    [Serializable]
    public struct AssetsDirectoryLibrary
    {
        /// <summary>
        /// This is a list of string for storing assets directory.
        /// </summary>
        [Header("Asset Library")]
        [Space(5)]
        public List<string> Directories;
    }
}