using System;

namespace NewsPost.Data.LogData
{
    public class LogRecord
    {
        public static string CreateLogStart(string functionName)
        {
            return $"Function: {functionName} is started.";
        }

        public static string CreateLogFinish(string functionName)
        {
            return $"Function: {functionName} is finished.";
        }

        public static string CreateLogRecord(string functionName, Exception ex)
        {
            return ex.InnerException == null
                ? $"Function: {functionName}; Exception: {ex.Message}."
                : $"Function: {functionName}; Exception: {ex.Message}; InnerException: {ex.InnerException.Message}.";
        }
    }
}
