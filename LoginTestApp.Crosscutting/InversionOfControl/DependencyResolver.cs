using System.Linq;
using LoginTestApp.Crosscutting.Contracts.InversionOfControl;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using DependencyOverride = LoginTestApp.Crosscutting.Contracts.InversionOfControl.DependencyOverride;
using ParameterOverride = LoginTestApp.Crosscutting.Contracts.InversionOfControl.ParameterOverride;
using Unity = Microsoft.Practices.Unity;

namespace LoginTestApp.Crosscutting.InversionOfControl
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly Unity.IUnityContainer container;

        public DependencyResolver(Unity.IUnityContainer container)
        {
            this.container = container;
        }

        public T Resolve<T>(params ParameterOverride[] parameters)
        {
            var overrides = new Unity.CompositeResolverOverride();
            overrides.AddRange(parameters.Select(x => new Unity.ParameterOverride(x.ParameterName, x.ParameterValue)));

            return container.Resolve<T>(overrides);
        }

        public T Resolve<T>(params object[] parameters)
        {
            var overrides = new Unity.CompositeResolverOverride();
            overrides.AddRange(parameters.Select(x => new Unity.DependencyOverride(x.GetType(), x)));

            return container.Resolve<T>(overrides);
        }

        public T Resolve<T>(params DependencyOverride[] parameters)
        {
            var overrides = new Unity.CompositeResolverOverride();
            parameters.ForEach(x => overrides.Add(new Unity.DependencyOverride(x.DependencyType, x.DependencyValue)));

            return container.Resolve<T>(overrides);
        }
    }
}
