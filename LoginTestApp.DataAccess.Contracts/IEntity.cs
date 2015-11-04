using System;

namespace LoginTestApp.DataAccess.Contracts
{
	public interface IEntity<TKey>
	{
		TKey Id { get; set; }

		string CreatedBy { get; set; }

		DateTime CreatedDate { get; set; }

		string LastModifiedBy { get; set; }

		DateTime LastModifiedDate { get; set; }
	}
}
