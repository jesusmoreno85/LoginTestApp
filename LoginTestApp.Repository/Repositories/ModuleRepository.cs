using System.Data.Entity;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Repository.Contracts.Repositories;

namespace LoginTestApp.Repository.Repositories
{
    public class ModuleRepository : RepositoryBase<Module, int, DataAccess.Contracts.Entities.Module>, IModuleRepository
    {
        public ModuleRepository(DbContext dbContext, IDataMapper dataMapper)
            : base(dbContext, dataMapper)
        {

        }
    }
}
