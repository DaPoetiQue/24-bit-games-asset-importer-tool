// Libraries
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Namespace
namespace AssetImporterToolkit
{
    // Asset import directories class
    public static class AssetImportDirectory
    {
        // Getting a asset directory from a given path
        public static string GetAssetDirectory(string path) => Path.GetDirectoryName(path);

        // Finding existing import configuration asset using filters 
        public static string[] FindImportConfigurations(string filter) => AssetDatabase.FindAssets(filter);

        // Find asset of type
        public static List<T> FindAssetsByType <T>() where T : Object
        {
            List<T> assets = new List<T>();

            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }

            // Returning found assets
            return assets;
        }
    }
}