// Used libraries.
using UnityEngine;
using UnityEditor;

// Namespace.
namespace AssetImporterToolkit
{
    // This is the asset importer configuration editor inspector class.
    [CustomEditor(typeof(ConfigurationAsset))]
    public class ConfigurationAssetEditorInspector : Editor
    {
        // Overiding the unity editors inspector GUI.
        public override void OnInspectorGUI()
        {
            // Displaying default unity inspector gui for the configuration scriptable object.
            base.OnInspectorGUI();

            // Checking if the asset configuration file's update button has been clicked by the user.
            if (GUILayout.Button("Update Assets", GUILayout.Height(35)))
            {
                // Getting the currently selected configuration file in the project.
                ConfigurationAsset[] configurationAsset = Selection.GetFiltered<ConfigurationAsset>(SelectionMode.Assets);

                // Checking if a configuration asset file is selected in the project.
                bool configurationAssetSelected = configurationAsset.Length > 0;

                // Checking if a configuration asset selected.
                if (configurationAssetSelected)
                {
                    // Log
                    Debug.Log("Selected : " + configurationAsset.Length + " configuration Files");

                    // Updating imported assets retroactively using the currently selected configuration asset at the first index.
                    Configurations.OnUpdateIncludedAssetsUsingConfiguration(configurationAsset[0]);
                }
            }
        }
    }
}