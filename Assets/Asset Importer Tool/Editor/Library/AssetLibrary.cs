using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssetImporterToolkit
{
    [Serializable]
    public struct AssetLibrary
    {
        [Header("Asset Library")]

        [Space(3)]
        public List<string> AssetDirectoryList;
    }
}