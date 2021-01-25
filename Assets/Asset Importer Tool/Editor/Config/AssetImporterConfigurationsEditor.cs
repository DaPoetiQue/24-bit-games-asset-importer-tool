// Libraries
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Asset importer configurations editor class
    public class AssetImporterConfigurationsEditor : Editor
    {
        // Asset Path
        public static string m_AssetImporterConfigurationsPath;

        // --Create Import Configuration
        [MenuItem("24 Bit Games/Asset Importer/Create/Configuration File")]
        private static void CreateImportConfiguration()
        {
            // Creating a new configuration asset instance
            var importConfigurations = CreateInstance<AssetImporterConfigurations>();

            // Get Path
            m_AssetImporterConfigurationsPath = EditorUtility.SaveFilePanelInProject("Asset Importer", "Asset Import Configuration", "asset", "");

            // --Create Asset
            AssetDatabase.CreateAsset(importConfigurations, m_AssetImporterConfigurationsPath);

            // --Get Asset
            importConfigurations.DefaultAssetImportConfigurationDirectory(AssetImportDirectory.GetAssetDirectory(m_AssetImporterConfigurationsPath));
        }
    }
}