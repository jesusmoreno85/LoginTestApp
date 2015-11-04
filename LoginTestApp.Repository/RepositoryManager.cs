using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.Repository.Contracts;

namespace LoginTestApp.Repository
{
    //TODO(AngelM): Consider to split this class in Modules
	public class RepositoryManager : IRepositoryManager
	{
		private readonly ILoginTestAppContext dbContext;
		private readonly IDataMapper dataMapper;
		private readonly DbContext dbContextInstance;

		public RepositoryManager(ILoginTestAppContext dbContext, IDataMapper dataMapper)
		{
			this.dbContext = dbContext;
			this.dataMapper = dataMapper;
			this.dbContextInstance = (DbContext) dbContext;
		}

		#region Repositories

		private IUsersRepository users;
		public IUsersRepository Users => (users ?? (users = new UsersRepository(this.dbContext, dataMapper)));

	    private IDynamicLinksRepository dynamicLinks;
		public IDynamicLinksRepository DynamicLinks => (dynamicLinks ?? (dynamicLinks = new  DynamicLinksRepository(this.dbContext, dataMapper)));

        #endregion  Repositories

        #region Public Methods

        public int SaveChanges()
		{
			return this.dbContextInstance.SaveChanges();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await this.dbContextInstance.SaveChangesAsync();
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			return await this.dbContextInstance.SaveChangesAsync(cancellationToken);
		}

        #endregion Public Methods

        #region IDisposable

        private bool disposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					dbContextInstance.Dispose();
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
