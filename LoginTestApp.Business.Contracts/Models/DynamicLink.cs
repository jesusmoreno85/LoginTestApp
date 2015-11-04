using System;

namespace LoginTestApp.Business.Contracts.Models
{
	public class DynamicLink : ModelBase
	{
		public Guid GuidId { get; set; }

		public string Type { get; set; }

		public string SubType { get; set; }

		public string Url { get; set; }

		public DateTime ExpiresOn { get; set; }

		public bool IsConsumed { get; set; }
	}
}
