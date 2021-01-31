using UnityEditor;

namespace AssetImporterToolkit
{
    public static class AssetsReimporter
    {
        private static string classLogName = "Assets Reimporter";

        public static void ReimportAssetsAtPath(string assetsPath, ConfigurationAsset configurationAsset)
        {
            string[] importableAssetFilesEntries = Utilities.GetMultipleExtensionFileEntries(assetsPath);

            bool importableAssetsFound = importableAssetFilesEntries.Length > 0;

            if (importableAssetsFound)
            {
                foreach (string importableAsset in importableAssetFilesEntries)
                {
                    AssetDatabase.ImportAsset(importableAsset);

                    if (configurationAsset.AllowDebug)
                    {
                        Debugger.Log(className : classLogName, message : " Asset at path : " + importableAsset + " has been updated successfully.");
                    }
                }
            }
            else
            {
                if (configurationAsset.AllowDebug)
                {
                    Debugger.LogWarning(className: classLogName, message: " No assets to update at path : " + assetsPath);
                }

                return;
            }

        }
    }
}