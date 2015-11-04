using LoginTestApp.Repository.Contracts.Repositories;

namespace LoginTestApp.Repository.Contracts
{
    public interface IAccountContext : IContext
    {
        IUsersRepository Users { get; }

        IDynamicLinksRepository DynamicLinks { get; }

        #region Methods

        #endregion Methods
    }
}
