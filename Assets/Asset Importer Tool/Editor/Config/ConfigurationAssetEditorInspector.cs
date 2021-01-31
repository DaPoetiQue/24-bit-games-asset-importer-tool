using UnityEngine;
using UnityEditor;

namespace AssetImporterToolkit
{
    [CustomEditor(typeof(ConfigurationAsset))]
    public class ConfigurationAssetEditorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update Assets", GUILayout.Height(30)))
            {
                ConfigurationAsset[] configurationAsset = Selection.GetFiltered<ConfigurationAsset>(SelectionMode.Assets);

                bool configurationAssetSelected = configurationAsset.Length > 0;

                if (configurationAssetSelected)
                {
                    Configurations.OnUpdateIncludedAssetsUsingConfiguration(configurationAsset[0]);
                }
            }
        }
    }
}