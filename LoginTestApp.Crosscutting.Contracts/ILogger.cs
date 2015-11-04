using System;

namespace LoginTestApp.Crosscutting.Contracts
{
    public interface ILogger
    {
        void LogError(string errorMessage, params object[] parameters);

        void LogException(Exception exception);
    }
}
