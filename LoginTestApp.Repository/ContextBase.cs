using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.Repository.Contracts;

namespace LoginTestApp.Repository
{
    public abstract class ContextBase : IContext 
    {
        protected readonly IDataMapper DataMapper;

        #region Private Members

        private readonly IDbContext dbContext;
        private readonly ConcurrentDictionary<Type, IRepository> repositories;
        private readonly ConcurrentDictionary<object, object> syncDictionary;

        #endregion Private Members

        #region Ctor

        protected ContextBase(IDbContext dbContext, IDataMapper dataMapper)
        {
            dbContext.OnSaveChanges += OnSaveChangesHandler;

            this.dbContext = dbContext;
            DataMapper = dataMapper;
            repositories = new ConcurrentDictionary<Type, IRepository>();

            syncDictionary = new ConcurrentDictionary<object, object>();
        }

        #endregion Ctor

        #region Protected Methods

        protected void OnSaveChangesHandler(int affectedRecords, List<DbEntityEntry> dbEntityEntries)
        {
            foreach (var entityToSync in dbEntityEntries
                .Where(t => t.State.Exists(EntityState.Added, EntityState.Modified))
                .Select(t => t.Entity)
                .ToList())
            {
                var syncItem = syncDictionary.Single(x => ReferenceEquals(x.Value, entityToSync)).Key;

                // ReSharper disable once RedundantAssignment
                //It will Sync the values on Model from Entity
                syncItem = DataMapper.MapTo(entityToSync.GetType(), syncItem.GetType());
            }
        }

        /// <summary>
        /// Gets an instance of the requested repository type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetRepository<T>() where T : IRepository
        {
            IRepository repository;
            Type requestedType = typeof(T);

            if (!repositories.TryGetValue(requestedType, out repository))
            {
                repository = (T)Activator.CreateInstance(requestedType, dbContext, DataMapper);
                repositories.TryAdd(requestedType, repository);

                ((IDataInteractions)repository).OnDataChange += OnOnDataChange;
            }

            return (T)repository;
        }

        private void OnOnDataChange(object model, object entity)
        {
            if (syncDictionary.ContainsKey(model))
            {
                object removedItem;
                syncDictionary.TryRemove(model, out removedItem);
            }

            syncDictionary.TryAdd(model, entity);
        }

        #endregion Private Methods

        #region IContext

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region IDisposable

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }
}
