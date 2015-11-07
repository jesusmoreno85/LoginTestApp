using System;
using System.Data.Entity;
using System.Linq;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Repository.Contracts.Repositories;
using Entities = LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.Repository.Repositories
{
    public class DynamicLinksRepository : RepositoryBase<DynamicLink, int, Entities.DynamicLink>, IDynamicLinksRepository
	{
		public DynamicLinksRepository(DbContext dbContext, IDataMapper dataMapper)
			: base(dbContext, dataMapper)
		{

		}

		public DynamicLink GetByGuidId(Guid guidId)
		{
			var link = DbSet.SingleOrDefault(x => x.GuidId == guidId);

			return DataMapper.MapTo<DynamicLink>(link);
		}
	}
}