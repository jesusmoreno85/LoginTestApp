using System.Data.Entity;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Repository.Contracts.Repositories;

namespace LoginTestApp.Repository.Repositories
{
    public class RoleRepository : RepositoryBase<Role, int, DataAccess.Contracts.Entities.Module>, IRoleRepository
    {
        public RoleRepository(DbContext dbContext, IDataMapper dataMapper) 
            : base(dbContext, dataMapper)
        {

        }
    }
}
