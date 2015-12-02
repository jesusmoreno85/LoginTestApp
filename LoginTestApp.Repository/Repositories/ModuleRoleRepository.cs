using System.Data.Entity;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Repository.Contracts.Repositories;

namespace LoginTestApp.Repository.Repositories
{
    public class ModuleRoleRepository : RepositoryBase<ModuleAction, string, DataAccess.Contracts.Entities.ModuleAction>, IModuleActionRepository
    {
        public ModuleRoleRepository(DbContext dbContext, IDataMapper dataMapper) : base(dbContext, dataMapper)
        {
        }
    }
}
