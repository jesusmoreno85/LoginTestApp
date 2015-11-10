using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Contracts;
using LoginTestApp.DataAccess.Contracts.Context;

namespace LoginTestApp.DataAccess.Context
{
    public abstract class DbContextBase : DbContext, IDbContext
    {
        private readonly ISystemContext systemContext;

        protected DbContextBase(string nameOrConnectionString, ISystemContext systemContext)
            : base(nameOrConnectionString)
        {
			this.systemContext = systemContext;
        }

        public event Action<int, List<DbEntityEntry>> OnSaveChanges;

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries()
                            .Where(t => t.Entity is IEntity 
                                && !t.State.Exists(EntityState.Detached, EntityState.Unchanged))
                            .ToList();

            entities
                .ForEach(dbEntry =>
                {
                    var baseEntity = (IEntity)dbEntry.Entity;

                    if (dbEntry.State == EntityState.Added)
                    {
                        baseEntity.CreatedDate = systemContext.DateTimeNow;
                        baseEntity.CreatedBy = systemContext.UserName;
                    }

                    baseEntity.LastModifiedDate = systemContext.DateTimeNow;
                    baseEntity.LastModifiedBy = systemContext.UserName;
                });

            int recordsAffected = base.SaveChanges();

            OnSaveChanges?.Invoke(recordsAffected, entities);

            return recordsAffected;
        }

    }
}
