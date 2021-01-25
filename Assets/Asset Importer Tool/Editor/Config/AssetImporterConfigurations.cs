// Libraries
using System.Collections.Generic;
using UnityEngine;

// Namespace
namespace AssetImporterToolkit
{
    // Asset Importer Configurations Scriptable Object Class
    public class AssetImporterConfigurations : ScriptableObject
    {
        // --Included Asset Directories
        [Space(3)]
        public List<string> m_IncludedAssetDirectory = new List<string>();

        // --Header
        [Space(3)]
        [Header("Texture Importer Settings")]

        [Space(3)]
        public Texture m_TextureImporterSettings;

        // --Overide Texture Settings For Android
        [Space(3)]
        public PlatformOption m_TextureOveridePlatormOption = PlatformOption.Android;

        // --Default Asset Import Configuration Directory
        public void DefaultAssetImportConfigurationDirectory(string path)
        {
            // --Assign New Path
            m_IncludedAssetDirectory.Add(path);
        }
    }
}