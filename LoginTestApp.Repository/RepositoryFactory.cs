using System.Data.Entity;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Repository.Contracts;
using Microsoft.Practices.Unity;

namespace LoginTestApp.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IUnityContainer container;

        public RepositoryFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public T Resolve<T>(DbContext dbContext, IDataMapper dataMapper)
        {
            var overrides = new CompositeResolverOverride
            {
                new DependencyOverride(typeof(DbContext), dbContext),
                new DependencyOverride(typeof(IDataMapper), dataMapper)
            };

            return container.Resolve<T>(overrides);
        }
    }
}