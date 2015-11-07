using System;
using System.Collections.Generic;

namespace LoginTestApp.Crosscutting.Contracts
{
    public interface IDependencyResolver
    {
        T Resolve<T>(params object[] parameters);

        T Resolve<T>(params KeyValuePair<string, object>[] parameters);

        T Resolve<T>(params KeyValuePair<Type, object>[] parameters);
    }
}
