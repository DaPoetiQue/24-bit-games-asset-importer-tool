// Libraries
using System;
using UnityEngine;

// Namespace
namespace AssetImporterToolkit
{
    // Debugger class
    public static class Debugger
    {
        // Log
        public static void Log(string className, object message)
        {
            // Log message to the console
            Debug.Log(FormattedLogMessage(className, message));
        }

        // Log warning function
        public static void LogWarning(string className, object message)
        {
            // Log message to the console
            Debug.LogWarning(FormattedLogMessage(className, message));
        }

        // Log error function
        public static void LogError(string className, object message)
        {
            // Log message to the console
            Debug.LogError(FormattedLogMessage(className, message));
        }

        // Throw exception
        public static void ThrowException(Exception exception)
        {
            // Throwing exception
            throw exception;
        }

        public static string FormattedLogMessage(string className, object message)
        {
            // Formated message
            string formatedMessage = string.Format("-->> Class Name : {0} , Log Message : {1} ", className, message);

            // Returning formatted message
            return formatedMessage;
        }
    }
}