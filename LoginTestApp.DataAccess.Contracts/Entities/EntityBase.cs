using System;

namespace LoginTestApp.DataAccess.Contracts.Entities
{
	/// <summary>
	/// Base object definition that contains the most common data
	/// </summary>
	public class EntityBase : IEntity<int>
	{
		public int Id { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedDate { get; set; }

		public string LastModifiedBy { get; set; }

		public DateTime? LastModifiedDate { get; set; }
	}
}
