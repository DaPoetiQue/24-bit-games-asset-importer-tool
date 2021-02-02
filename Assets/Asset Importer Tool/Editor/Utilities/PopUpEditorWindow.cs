// Used libraries.
using UnityEditor;

// Namespace.
namespace AssetImporterToolkit
{
    /// <summary>
    /// This class displays a unity dialog pop up editor window.
    /// </summary>
    public class PopUpEditorWindow : EditorWindow
    {
        // This log name is used when logging messages to the debbuger.
        private static string classLogName = "Asset Importer";

        /// <summary>
        /// This function displays a unity dialog pop up window.
        /// </summary>
        /// <param name="title">The title is the text displayed at the top of the dialog box header section.</param>
        /// <param name="message">The dialog displayed message.</param>
        /// <param name="success">The message displayed whenn the user press ok.</param>
        /// <param name="configurationAsset"></param>
        public static void OpenConfigurationADisplayDialogWindow(string title, string message, ConfigurationAsset configurationAsset)
        {
            // checkeing if the configuration file is valid.
            bool isValidConfigurationAssetFile = AssetImportDirectory.IsValidConfigurationAsset(configurationAsset);

            if(isValidConfigurationAssetFile)
            {
                // Displaying a unity dialog pop up window.
                if (EditorUtility.DisplayDialog(title, message, "Ok", "Cancel"))
                {

                    // Logging a success message to the unity debug console window.
                    Debugger.Log(className: classLogName, message: "success");
                }
                else
                {
                    // Logging a warning message to the unity debug console window.
                    // Debugger.LogWarning(className : classLogName);
                }
            }
            else
            {
                return;
            }
        }
    }
}