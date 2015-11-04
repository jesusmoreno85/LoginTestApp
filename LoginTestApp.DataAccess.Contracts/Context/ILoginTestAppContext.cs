using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Contracts.Context
{
	public interface ILoginTestAppContext
	{
		#region Properties

		IDbSet<User> Users { get; set; }

		IDbSet<DynamicLink> DynamicLinks { get; set; }

		#endregion Properties

		//#region Methods

		//int SaveChanges();

		//Task<int> SaveChangesAsync();

		//Task<int> SaveChangesAsync(CancellationToken cancellationToken);

		//DbSet<TEntity> DbSet<TEntity>() where TEntity : class;

		//#endregion Methods
	}
}
