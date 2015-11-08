using System;
using System.Data.Entity;
using System.Linq;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Repository.Contracts.Repositories;
using Entities = LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.Repository.Repositories
{
    public class UsersRepository : RepositoryBase<User, int, Entities.User>, IUsersRepository
    {
        public UsersRepository(DbContext dbContext, IDataMapper dataMapper)
            : base(dbContext, dataMapper)
        {

        }

        public User FindUserByAlias(string alias, bool? isActive = true)
        {
            var user = this.DbSet.SingleOrDefault(x =>
                x.Alias == alias &&
                (!isActive.HasValue || isActive.Value == x.IsActive));
            
            return this.DataMapper.MapTo<User>(user);
        }
    }
}