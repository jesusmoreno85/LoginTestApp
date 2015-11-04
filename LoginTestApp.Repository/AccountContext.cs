using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.Repository.Contracts;
using LoginTestApp.Repository.Contracts.Repositories;

namespace LoginTestApp.Repository
{
    public class AccountContext : ContextBase, IAccountContext
    {
        public AccountContext(ILoginTestAppContext dbContext, IDataMapper dataMapper) 
            : base(dbContext, dataMapper)
        {
        }

        #region Repositories

        public IUsersRepository Users => GetRepository<IUsersRepository>();

        public IDynamicLinksRepository DynamicLinks => GetRepository<IDynamicLinksRepository>();

        #endregion Repositories
    }
}
