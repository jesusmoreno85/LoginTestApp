using System;

namespace LoginTestApp.DataAccess.Contracts.Entities
{
	/// <summary>
	/// Represents the different dynamic web links 
	/// </summary>
	public class DynamicLink : EntityBase<int>
    {
		public Guid GuidId { get; set; }

		public string Type { get; set; }

		public string SubType { get; set; }

		public string Url { get; set; }

		public DateTime ExpiresOn { get; set; }

		public bool IsConsumed { get; set; }
	}
}
