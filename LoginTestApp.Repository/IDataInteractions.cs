using System;

namespace LoginTestApp.Repository
{
    internal interface IDataInteractions
    {
        event Action<object, object> OnDataChange;
    }
}