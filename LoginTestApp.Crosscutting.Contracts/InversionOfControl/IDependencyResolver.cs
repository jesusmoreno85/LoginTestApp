
namespace LoginTestApp.Crosscutting.Contracts.InversionOfControl
{
    public interface IDependencyResolver
    {
        /// <summary>
        /// Resolves a dependency based on a dependency override
        /// </summary>
        /// <typeparam name="T">Dependency type to resolve</typeparam>
        /// <param name="name">The named registered type</param>
        /// <param name="parameters">The dependency overrides</param>
        T Resolve<T>(string name, params DependencyOverride[] parameters);

        /// <summary>
        /// Resolves a dependency based on a dependency override
        /// </summary>
        /// <typeparam name="T">Dependency type to resolve</typeparam>
        /// <param name="parameters">The dependency overrides</param>
        T Resolve<T>(params DependencyOverride[] parameters);
    }
}
