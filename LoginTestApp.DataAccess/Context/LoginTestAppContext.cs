using System.Data.Entity;
using System.Linq;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Configuration;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Context
{
	public class LoginTestAppContext : DbContext, ILoginTestAppContext
	{
		private readonly ISystemContext systemContext;

		public LoginTestAppContext(string nameOrConnectionString, ISystemContext systemContext)
			: base(nameOrConnectionString)
		{
			this.systemContext = systemContext;
			Configuration.LazyLoadingEnabled = false;
			Configuration.ProxyCreationEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new UserMapping());
		}

		#region ILoginTestAppContext

		public IDbSet<User> Users { get; set; }

		public IDbSet<DynamicLink> DynamicLinks { get; set; }

		#endregion ILoginTestAppContext

		public override int SaveChanges()
		{
			var entities = ChangeTracker.Entries()
							.Where(t =>
								t.Entity is EntityBase &&
								(t.State == EntityState.Added || t.State == EntityState.Modified))
							.ToList();

			entities
				.ForEach(dbEntry =>
				{
					var baseEntity = (EntityBase)dbEntry.Entity;

					if (dbEntry.State == EntityState.Added)
					{
						baseEntity.CreatedDate = systemContext.DateTimeNow;
						baseEntity.CreatedBy = systemContext.UserName;
					}

					baseEntity.LastModifiedDate = systemContext.DateTimeNow;
					baseEntity.LastModifiedBy = systemContext.UserName;
				});

			return base.SaveChanges();
		}
	}
}