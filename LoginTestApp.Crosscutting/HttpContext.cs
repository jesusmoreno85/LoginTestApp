using System;
using System.Web.Hosting;
using LoginTestApp.Crosscutting.Contracts;

namespace LoginTestApp.Crosscutting
{
    public class HttpContext : ISystemContext
    {
        public HttpContext(string appFullName)
        {
            AppFullName = appFullName;
        }

        public string AppFullName { get; private set; }

        public string UserName
        {
            get { return System.Web.HttpContext.Current.User.Identity.Name; }
        }

        public DateTime DateTimeNow
        {
            get { return DateTime.UtcNow; }
        }

        public string MapPath(string virtualPath)
        {
            return HostingEnvironment.MapPath(virtualPath);
        }
    }
}
