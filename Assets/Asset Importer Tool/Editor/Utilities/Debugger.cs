using System;
using UnityEngine;

namespace AssetImporterToolkit
{
    public static class Debugger
    {
        public static void Log(string className, object message)
        {
            Debug.Log(FormattedLogMessage(className, message));
        }

        public static void LogWarning(string className, object message)
        {
            Debug.LogWarning(FormattedLogMessage(className, message));
        }

        public static void LogError(string className, object message)
        {
            Debug.LogError(FormattedLogMessage(className, message));
        }

        public static void ThrowException(Exception exception)
        {
            throw exception;
        }

        public static string FormattedLogMessage(string className, object message)
        {
            string formatedMessage = string.Format("-->> Class Name : {0} , Log Message : {1} ", className, message);

            return formatedMessage;
        }
    }
}