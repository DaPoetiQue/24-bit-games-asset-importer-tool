// Used libraries.
using System;
using UnityEngine;

// Namespace.
namespace AssetImporterToolkit
{
    // Debugger class
    /// <summary>
    /// This class is responsible for logging formatted messages to the unity debug console window.
    /// </summary>
    public static class Debugger
    {
        /// <summary>
        /// Logs a new message to te unity debug console window.
        /// </summary>
        /// <param name="className">Takes in a string parameter for the class name to be used when printing to the debug console.</param>
        /// <param name="message">Takes in a string parameter for the message to be logged in the console window.</param>
        public static void Log(string className, object message)
        {
            // Log new message to the unity debug console.
            Debug.Log(FormattedLogMessage(className, message));
        }

        /// <summary>
        /// Logs a new warning message to te unity debug console window.
        /// </summary>
        /// <param name="className">Takes in a string parameter for the class name to be used when printing to the debug console.</param>
        /// <param name="message">Takes in a string parameter for the message to be logged in the console window.</param>
        public static void LogWarning(string className, object message)
        {
            // Log new warning message to the unity debug console.
            Debug.LogWarning(FormattedLogMessage(className, message));
        }

        /// <summary>
        /// Logs a new error message to te unity debug console window.
        /// </summary>
        /// <param name="className">Takes in a string parameter for the class name to be used when printing to the debug console.</param>
        /// <param name="message">Takes in a string parameter for the message to be logged in the console window.</param>
        public static void LogError(string className, object message)
        {
            // Log new error message to the unity debug console.
            Debug.LogError(FormattedLogMessage(className, message));
        }

        /// <summary>
        /// This function throws a new exception to the unity debug console window.
        /// </summary>
        /// <param name="exception">Takes in a system exception to throw.</param>
        public static void ThrowException(Exception exception)
        {
            // Throwing a system exception.
            throw exception;
        }

        /// <summary>
        /// This function is used to format messages.
        /// </summary>
        /// <param name="className">Takes in a string parameter for the class name to be used when printing to the debug console.</param>
        /// <param name="message">Takes in a string parameter for the message to be logged in the console window.</param>
        /// <returns></returns>
        public static string FormattedLogMessage(string className, object message)
        {
            // Formatting a log message.
            string formatedMessage = string.Format("-->> Class Name : {0} , Log Message : {1} ", className, message);

            // Returning formatted message.
            return formatedMessage;
        }
    }
}