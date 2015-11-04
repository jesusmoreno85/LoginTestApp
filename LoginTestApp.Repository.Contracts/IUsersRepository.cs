using System;
using LoginTestApp.Business.Contracts.Models;

namespace LoginTestApp.Repository.Contracts
{
	public interface IUsersRepository : IRepository<User, int>
	{
		User FindUserByAlias(string alias, bool? isActive = true);
	}
}