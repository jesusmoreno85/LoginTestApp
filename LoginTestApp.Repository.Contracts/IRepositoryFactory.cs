using System.Data.Entity;
using LoginTestApp.Crosscutting.Contracts;

namespace LoginTestApp.Repository.Contracts
{
    public interface IRepositoryFactory
    {
        T Resolve<T>(DbContext dbContext, IDataMapper dataMapper);
    }
}
