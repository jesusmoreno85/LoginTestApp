using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace LoginTestApp.DataAccess.Contracts.Context
{
    public interface IDbContext
    {
        event Action<int, List<DbEntityEntry>> OnSaveChanges;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void Dispose();
    }
}

