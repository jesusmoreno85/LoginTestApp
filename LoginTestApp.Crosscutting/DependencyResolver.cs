using System;
using System.Collections.Generic;
using LoginTestApp.Crosscutting.Contracts;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace LoginTestApp.Crosscutting
{
    //TODO(AngelM): We should probably remove this stuff
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;

        public DependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public T Resolve<T>(params object[] parameters)
        {
            var overrides = new CompositeResolverOverride();
            parameters.ForEach(x => overrides.Add(new DependencyOverride(x.GetType(), x)));

            return container.Resolve<T>(overrides);
        }

        public T Resolve<T>(params KeyValuePair<string, object>[] parameters)
        {
            var overrides = new CompositeResolverOverride();
            parameters.ForEach(x => overrides.Add(new ParameterOverride(x.Key, x.Value)));

            return container.Resolve<T>(overrides);
        }

        public T Resolve<T>(params KeyValuePair<Type, object>[] parameters)
        {
            var overrides = new CompositeResolverOverride();
            parameters.ForEach(x => overrides.Add(new DependencyOverride(x.Key, x.Value)));

            return container.Resolve<T>(overrides);
        }
    }
}
