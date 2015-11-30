using System;

namespace LoginTestApp.DataAccess.Contracts
{
	public interface IEntity<TKey> : IEntity
	{
		TKey Id { get; set; }
	}

    public interface IEntity
    {
        string CreatedBy { get; set; }

        DateTime? CreatedDate { get; set; }

        string LastModifiedBy { get; set; }

        DateTime? LastModifiedDate { get; set; }
    }
}
