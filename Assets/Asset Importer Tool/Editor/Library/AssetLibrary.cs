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
    /// <typeparam name="T">Takes in a object type, i.e. string</typeparam>
    [Serializable]
    public struct AssetLibrary<T>
    {
        /// <summary>
        /// This is a list for storing asset directory.
        /// </summary>
        [Header("Asset Library")]
        [Space(10)]
        public List<T> AssetDirectoryList;
    }
}