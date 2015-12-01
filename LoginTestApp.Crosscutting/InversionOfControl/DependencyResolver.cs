using LoginTestApp.Crosscutting.Contracts.InversionOfControl;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using DependencyOverride = LoginTestApp.Crosscutting.Contracts.InversionOfControl.DependencyOverride;
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

        public T Resolve<T>(string name, params DependencyOverride[] parameters)
        {
            var overrides = BuildUnityOverrides(parameters);

            return container.Resolve<T>(name, overrides);
        }

        public T Resolve<T>(params DependencyOverride[] parameters)
        {
            var overrides = BuildUnityOverrides(parameters);

            return container.Resolve<T>(overrides);
        }

        /// <summary>
        /// Constructs the Unity overrides based on the local DependencyOverride
        /// </summary>
        /// <param name="parameters">The paramters data to override</param>
        private Unity.CompositeResolverOverride BuildUnityOverrides(params DependencyOverride[] parameters)
        {
            var overrides = new Unity.CompositeResolverOverride();

            parameters.ForEach(x =>
            {
                if (!string.IsNullOrWhiteSpace(x.DependencyName))
                {
                    overrides.Add(new Unity.ParameterOverride(x.DependencyName, x.DependencyValue));
                }
                else
                {
                    overrides.Add(new Unity.DependencyOverride(x.DependencyType, x.DependencyValue));
                }
            });

            return overrides;
        }
    }
}
