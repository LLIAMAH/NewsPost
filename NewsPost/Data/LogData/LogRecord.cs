using System;

namespace NewsPost.Data.LogData
{
    public class LogRecord
    {
        public static string CreateLogStart(string name)
        {
            return $"Function: {name} is started.";
        }

        public static string CreateLogFinish(string name)
        {
            return $"Function: {name} is finished.";
        }

        public static string CreateLog(string name, Exception exception)
        {
            return
                $"Function: {name};{Environment.NewLine}Exception: {exception.Message};{Environment.NewLine}Inner exception: {exception.InnerException?.Message}.";
        }
    }
}
