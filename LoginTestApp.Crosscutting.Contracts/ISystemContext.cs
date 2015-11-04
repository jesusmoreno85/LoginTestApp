using System;

namespace LoginTestApp.Crosscutting.Contracts
{
    public interface ISystemContext
    {
        string AppFullName { get; }

        string UserName { get; }

        DateTime DateTimeNow { get; }

        string MapPath(string virtualPath);
    }
}
