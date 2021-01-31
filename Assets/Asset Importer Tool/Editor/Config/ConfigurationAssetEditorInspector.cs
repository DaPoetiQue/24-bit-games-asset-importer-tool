// Libraries
using UnityEngine;
using UnityEditor;

// --Namespace
namespace AssetImporterToolkit
{
    // Asset importer configuration editor inspector class.
    [CustomEditor(typeof(ConfigurationAsset))]
    public class ConfigurationAssetEditorInspector : Editor
    {
        // On inspector GUI method for the invoking the assets update function
        public override void OnInspectorGUI()
        {
            // Displaying default unity inspector gui.
            base.OnInspectorGUI();

            // Checking if the asset configuration file's update button has been clicked by the user.
            if (GUILayout.Button("Update Assets", GUILayout.Height(30)))
            {
                // Getting the currently selected configuration file in the project.
                ConfigurationAsset[] configurationAsset = Selection.GetFiltered<ConfigurationAsset>(SelectionMode.Assets);

                // Checking if a configuration asset file is selected in the project.
                bool configurationAssetSelected = configurationAsset.Length > 0;

                // Checking if a configuration asset file is selected in the project.
                if (configurationAssetSelected)
                {
                    // Updating imported assets retroactively using the currently selected configuration asset at the first index
                    Configurations.OnUpdateIncludedAssetsUsingConfiguration(configurationAsset[0]);
                }
            }
        }
    }
}