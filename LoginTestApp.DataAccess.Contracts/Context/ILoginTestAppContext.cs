using System.Data.Entity;
using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Contracts.Context
{
	public interface ILoginTestAppContext : IDbContext
	{
		#region Properties

		IDbSet<User> Users { get; set; }

		IDbSet<DynamicLink> DynamicLinks { get; set; }

		#endregion Properties
	}
}
