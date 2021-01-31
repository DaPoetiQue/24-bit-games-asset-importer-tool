using UnityEditor;

namespace AssetImporterToolkit
{
    public class ConfigurationAssetCreator : Editor
    {
        private static string classLogName = "Configuration Asset Creator";

        [MenuItem("24 Bit Games/Asset Importer/Create Configuration Asset")]
        private static void CreateImportConfiguration()
        {
            ConfigurationAsset assetImportConfigurationAsset = CreateInstance<ConfigurationAsset>();

            string importConfigurationAssetSavePath = EditorUtility.SaveFilePanelInProject("Asset Importer", "Asset Import Configuration", "asset", "");

            bool configurationAssetSavePathExist = !string.IsNullOrEmpty(importConfigurationAssetSavePath);

            bool isConfigurationAssetCreated = assetImportConfigurationAsset != null;

            if (isConfigurationAssetCreated && configurationAssetSavePathExist)
            {
                string configurationAssetCreationDirectory = AssetImportDirectory.GetAssetDirectory(importConfigurationAssetSavePath);

                AssetDatabase.CreateAsset(assetImportConfigurationAsset, importConfigurationAssetSavePath);

                Configurations.AddConfigurationIncludedAssetDirectory(assetImportConfigurationAsset, configurationAssetCreationDirectory);
            }
            else
            {
                Debugger.LogWarning(className : classLogName, message : "Asset importer toolkit : Configuration file creation operation canceled/failed: Import configuration asset file not created");

                return;
            }
        }
    }
}