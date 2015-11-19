using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Crosscutting.Contracts.InversionOfControl;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.Repository.Contracts;
using LoginTestApp.Repository.Contracts.Repositories;

namespace LoginTestApp.Repository
{
    /// <summary>
    /// Controls all the Repository access for Account stuffs
    /// </summary>
    public class AccountContext : ContextBase, IAccountContext
    {
        public AccountContext(ILoginTestAppContext dbContext, IDataMapper dataMapper, IDependencyResolver dependencyResolver) 
            : base(dbContext, dataMapper, dependencyResolver)
        {
        }

        #region Repositories

        public IUsersRepository Users => GetRepository<IUsersRepository>();

        public IDynamicLinksRepository DynamicLinks => GetRepository<IDynamicLinksRepository>();

        #endregion Repositories
    }
}
