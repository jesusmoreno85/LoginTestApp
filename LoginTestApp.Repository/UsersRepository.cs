using System;
using System.Data.Entity;
using System.Linq;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.Repository.Contracts;
using Entities = LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.Repository
{
	class UsersRepository : RepositoryBase<User, int, Entities.User>, IUsersRepository
	{
		public UsersRepository(ILoginTestAppContext dbContext, IDataMapper dataMapper) 
			: base((DbContext)dbContext, dataMapper)
		{

		}

		public User FindUserByAlias(string alias, bool? isActive = true)
		{
			var user = this.DbSet.SingleOrDefault(x =>
				x.Alias == alias &&
                isActive.HasNoValueOrEquals(x.IsActive));

            return DataMapper.MapTo<User>(user);
		}
	}
}