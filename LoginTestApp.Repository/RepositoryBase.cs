using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LoginTestApp.Business.Contracts;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Contracts;
using LoginTestApp.Repository.Contracts;
using LoginTestApp.Repository.ExtensionMethods;

namespace LoginTestApp.Repository
{
    /// <summary>
    /// Base class that defines the most common operations for Model objects
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TKey">Model's primary key type</typeparam>
    /// <typeparam name="TEntity">DataAcess Entity Type to Map To and Map From</typeparam>
	public abstract class RepositoryBase<TModel, TKey, TEntity> : IRepository<TModel, TKey>, IDataInteractions
        where TModel : class, IModel<TKey>
        where TEntity : class, IEntity<TKey>
    {
        #region Private Members

        private readonly string tableName;
		
		private readonly string keyColumnName;

        #endregion Private Members

        public event Action<object, object> OnDataChange;

        #region Protected Members

        protected readonly IDbSet<TEntity> DbSet;

		protected readonly IDataMapper DataMapper;

		protected readonly DbContext DbContext;

		#endregion Protected Members

		protected RepositoryBase(DbContext dbContext, IDataMapper dataMapper)
		{
			if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));

			if (dataMapper == null) throw new ArgumentNullException(nameof(dataMapper));

            DbContext = dbContext;
			DbSet = dbContext.Set<TEntity>();
			DataMapper = dataMapper;

			tableName = dbContext.GetTableName<TEntity>();
			keyColumnName = dbContext.GetKeyPropertiesNames<TEntity>().Single();
		}

        protected void RaiseOnDataChange(TModel model, TEntity entity)
        {
            OnDataChange?.Invoke(model, entity);
        }

        public void Create(TModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			var entity = DataMapper.MapTo<TEntity>(model);

		    DbSet.Add(entity);
            RaiseOnDataChange(model, entity);
        }

        public TModel GetById(TKey id)
		{
			var entity = DbSet.Find(id);

			var model = DataMapper.MapTo<TModel>(entity);

			return model;
		}

		public void Delete(TModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			var entity = DataMapper.MapTo<TEntity>(model);

			DbSet.Attach(entity);
			DbSet.Remove(entity);
		}

		public int DeleteById(TKey id)
		{
			if (Comparer<TKey>.Default.Compare(id, default(TKey)) == 0) return 0;

			int recordsAffected;

		    Type idType = typeof (TKey);

            if (idType == typeof(int))
			{
				if (Convert.ToInt32(id) == 0) throw new ArgumentException("id has a valid value of 0");

				recordsAffected = DbContext.Database.ExecuteSqlCommand("DELETE FROM {0} WHERE {1} = {2}", tableName, keyColumnName, id);
			}
			else if (idType == typeof(string) 
                || idType == typeof(Guid))
			{
				if (string.IsNullOrWhiteSpace(id.ToString())) throw new ArgumentException($"id has a valid value of '{id}'");

				recordsAffected = DbContext.Database.ExecuteSqlCommand("DELETE FROM {0} WHERE {1} = '{2}'", tableName, keyColumnName, id);
			}
			else
			{
				throw new Exception("There is no supported key type for the Delete operation");
			}

			return recordsAffected;
		}

		public void Update(TModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			var entity = DataMapper.MapTo<TEntity>(model);

            DbSet.Attach(entity);
			DbContext.Entry(entity).State = EntityState.Modified;
            RaiseOnDataChange(model, entity);
        }
	}
}
