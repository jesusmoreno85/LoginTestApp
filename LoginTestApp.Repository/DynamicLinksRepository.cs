using System;
using System.Data.Entity;
using System.Linq;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.Repository.Contracts;
using Entities = LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.Repository
{
	class DynamicLinksRepository : RepositoryBase<DynamicLink, int, Entities.DynamicLink>, IDynamicLinksRepository
	{
		public DynamicLinksRepository(ILoginTestAppContext dbContext, IDataMapper dataMapper)
			: base((DbContext)dbContext, dataMapper)
		{

		}

		public DynamicLink GetByGuidId(Guid guidId)
		{
			var link = DbSet.SingleOrDefault(x => x.GuidId == guidId);

			return DataMapper.MapTo<DynamicLink>(link);
		}
	}
}