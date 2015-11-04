using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoginTestApp.Repository.Contracts
{
	public interface IRepositoryManager : IDisposable
	{
		IUsersRepository Users { get; }

		IDynamicLinksRepository DynamicLinks { get; }
		
		int SaveChanges();

		Task<int> SaveChangesAsync();

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);

	    void Dispose();
	}
}