using System;
using System.Collections.Concurrent;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Repository.Contracts;

namespace LoginTestApp.Repository
{
    public abstract class ContextBase : IContext 
    {
        protected readonly IDataMapper DataMapper;

        #region Private Members

        private readonly DbContext dbContext;
        private readonly ConcurrentDictionary<Type, object> repositories;

        #endregion Private Members

        #region Ctor

        protected ContextBase(DbContext dbContext, IDataMapper dataMapper)
        {
            this.dbContext = dbContext;
            this.DataMapper = dataMapper;
            this.repositories = new ConcurrentDictionary<Type, object>();
        }

        #endregion Ctor

        #region Protected Methods

        /// <summary>
        /// Gets an instance of the requested repository type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetRepository<T>() where T : IRepository
        {
            object repository;
            Type requestedType = typeof(T);

            if (!repositories.TryGetValue(requestedType, out repository))
            {
                repository = Activator.CreateInstance(requestedType, this.dbContext, this.DataMapper);
                repositories.TryAdd(requestedType, repository);
            }

            return (T)repository;
        }

        #endregion Private Methods

        #region IContext

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region IDisposable

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }
}
