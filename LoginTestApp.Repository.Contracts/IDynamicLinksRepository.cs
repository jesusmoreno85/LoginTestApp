using System;
using LoginTestApp.Business.Contracts.Models;

namespace LoginTestApp.Repository.Contracts
{
	public interface IDynamicLinksRepository : IRepository<DynamicLink, int>
    {
		DynamicLink GetByGuidId(Guid guidId);
	}
}