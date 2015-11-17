using LoginTestApp.Business.Contracts.Models;

namespace LoginTestApp.Repository.Contracts.Repositories
{
	public interface IUsersRepository : IRepository<User, int>
	{
        User FindUserByAlias(string alias, bool? isActive = null);
    }
}