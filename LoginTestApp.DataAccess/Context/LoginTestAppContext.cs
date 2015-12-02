using System.Data.Entity;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Configuration;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Context
{
	public class LoginTestAppContext : DbContextBase, ILoginTestAppContext
	{
		public LoginTestAppContext(string nameOrConnectionString, ISystemContext systemContext)
			: base(nameOrConnectionString, systemContext)
		{
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

        public IDbSet<Module> Modules { get; set; }

        public IDbSet<ModuleAction> ModuleActions { get; set; }

        public IDbSet<Role> Roles { get; set; }

        #endregion ILoginTestAppContext
    }
}