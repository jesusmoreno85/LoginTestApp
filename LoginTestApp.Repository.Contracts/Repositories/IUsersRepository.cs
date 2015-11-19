using LoginTestApp.Business.Contracts.Models;

namespace LoginTestApp.Repository.Contracts.Repositories
{
    /// <summary>
    /// Handles Domain User Models operations
    /// </summary>
	public interface IUsersRepository : IRepository<User, int>
	{
        /// <summary>
        /// Looks for a user with a given alias
        /// </summary>
        /// <param name="alias">The user's alias</param>
        /// <param name="isActive">Indicates an active state to check. If null then this parameter gets ignored</param>
        /// <returns>Null if the user wasn't found</returns>
        User FindUserByAlias(string alias, bool? isActive = null);

        /// <summary>
        /// Determines if the given alias is available
        /// </summary>
        /// <param name="alias">The user's alias</param>
        /// <param name="id">The user id reference. New users must have Zero.</param>
        /// <returns>True if the given alias does not exist in another user</returns>
	    bool IsAliasAvailable(string alias, int id = 0);
	}
}