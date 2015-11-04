using System;
using System.Diagnostics;
using System.Text;
using LoginTestApp.Crosscutting.Contracts;

namespace LoginTestApp.Crosscutting
{
    public class Logger : ILogger
    {
        private readonly string applicationName;

        public Logger(string applicationName, string targetLog)
        {
            if (applicationName == null) throw new ArgumentNullException("applicationName");

            if (targetLog == null) throw new ArgumentNullException("targetLog");

            //Everything is OK with the given parameters
            this.applicationName = applicationName;

            if (!EventLog.SourceExists(this.applicationName))
            {
                EventLog.CreateEventSource(this.applicationName, targetLog);
            }
        }

        public void LogError(string errorMessage, params object[] parameters)
        {
            EventLog.WriteEntry(this.applicationName, string.Format(errorMessage, parameters), EventLogEntryType.Error);
        }

        public void LogException(Exception exception)
        {
            LogError(GetFormattedExceptionMessage(exception));
        }

        private static string GetFormattedExceptionMessage(Exception ex)
        {
            var stringBuilder = new StringBuilder(ApplyStringFormatToException(ex));

            Exception innerException = ex.InnerException;
            int tabCounter = 1;

            do
            {
                stringBuilder.Append(ApplyStringFormatToException(ex.InnerException, tabCounter));

                if (innerException != null)
                {
                    innerException = innerException.InnerException;
                    tabCounter++;
                }
            } while (innerException != null);

            return stringBuilder.ToString();
        }

        private static string ApplyStringFormatToException(Exception ex, int tabCounter = 0)
        {
            if (ex == null) return "null";

            var newLineAndTabs = "\r\n" + new string('\t', tabCounter);

            string formattedException = string.Format(
                "{4}ExceptionType: {0}{4}Source: {1}{4}Message: {2}{4}StackTrace: {3}{4}Inner Exception: "
                , ex.GetType()
                , ex.Source
                , ex.Message
                , ex.StackTrace
                , newLineAndTabs);

            return formattedException;
        }
    }
}
