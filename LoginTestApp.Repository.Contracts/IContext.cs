using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoginTestApp.Repository.Contracts
{
    public interface IContext : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
