using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LoginTestApp.Business.Contracts;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Contracts;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.Repository.Contracts;

namespace LoginTestApp.Repository
{
    public abstract class ContextBase : IContext 
    {
        protected readonly IDataMapper DataMapper;
        
        #region Private Members

        private readonly IDbContext dbContext;
        private readonly IDependencyResolver dependencyResolver;
        private readonly ConcurrentDictionary<Type, IRepository> repositories;
        private readonly ConcurrentDictionary<IModel, IEntity> syncDictionary;

        #endregion Private Members

        #region Ctor

        protected ContextBase(IDbContext dbContext, IDataMapper dataMapper, IDependencyResolver dependencyResolver)
        {
            dbContext.OnSaveChanges += OnSaveChangesHandler;

            this.dbContext = dbContext;
            DataMapper = dataMapper;
            this.dependencyResolver = dependencyResolver;
            repositories = new ConcurrentDictionary<Type, IRepository>();

            syncDictionary = new ConcurrentDictionary<IModel, IEntity>();
        }

        #endregion Ctor

        #region Protected Methods

        protected void OnSaveChangesHandler(int affectedRecords, List<DbEntityEntry> dbEntityEntries)
        {
            foreach (var sourceEntity in dbEntityEntries
                .Select(t => t.Entity)
                .ToList())
            {
                var modelToSync = syncDictionary.Single(x => ReferenceEquals(x.Value, sourceEntity)).Key;

                //It will Sync the values on Model from Entity
                modelToSync = (IModel)DataMapper.MapTo(sourceEntity, modelToSync);

                IEntity entity;
                syncDictionary.TryRemove(modelToSync, out entity);
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
                //TODO(AngelM): Have to check this as I don't like the current approach
                var pameterOverrides = new []
                {
                    new KeyValuePair<Type, object>(typeof(DbContext), (DbContext)dbContext),
                    new KeyValuePair<Type, object>(typeof(IDataMapper), DataMapper),
                };

                repository = dependencyResolver.Resolve<T>(pameterOverrides);
                repositories.TryAdd(requestedType, repository);

                ((IDataInteractions)repository).OnDataChange += OnOnDataChange;
            }

            return (T)repository;
        }

        private void OnOnDataChange(IModel model, IEntity entity)
        {
            if (syncDictionary.ContainsKey(model))
            {
                IEntity removedItem;
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
