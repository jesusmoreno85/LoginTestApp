using System;

namespace LoginTestApp.Business.Contracts.Models
{
	public class ModelBase<T> : ModelBase, IModel<T>
	{
		public T Id { get; set; }
    }

    public class ModelBase : IModel
    {
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
