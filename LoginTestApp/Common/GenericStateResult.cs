using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LoginTestApp.Business.Contracts.BusinessOperation;

namespace LoginTestApp.Common
{
	/// <summary>
	/// Represents a JsonResult interaction to send information to the client
	/// </summary>
	public class GenericStateResult : JsonResult
	{
        public GenericStateResult()
        {
            IsError = false;
            Messages = new List<BusinessMessage>();
            InnerData = null;
        }

        public GenericStateResult(List<BusinessMessage> messages, object innerData = null)
        {
            IsError = messages.Any(x => x.Level == BusinessMessageLevel.Error);
		    Messages = messages;
            InnerData = innerData;
		}

		public bool IsError { get; set; }

		public List<BusinessMessage> Messages { get; set; }

        public object InnerData { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			var jsonResponse = new JsonResult
			{
				Data = this,
				ContentType = "application/json",
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};

			jsonResponse.ExecuteResult(context);
		}
	}
}