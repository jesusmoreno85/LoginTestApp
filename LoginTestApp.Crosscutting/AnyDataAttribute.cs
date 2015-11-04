using System;

namespace LoginTestApp.Crosscutting
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class AnyDataAttribute : Attribute
    {
        public object Data { get; set; }
    }
}
