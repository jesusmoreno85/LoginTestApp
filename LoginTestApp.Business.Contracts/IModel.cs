using System;

namespace LoginTestApp.Business.Contracts
{
	public interface IModel<TKey> : IModel
    {
		TKey Id { get; set; }
	}

    public interface IModel
    {
        string CreatedBy { get; set; }

        DateTime? CreatedDate { get; set; }

        string LastModifiedBy { get; set; }

        DateTime? LastModifiedDate { get; set; }
    }
}
