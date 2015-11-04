using System.Web.Mvc;

namespace LoginTestApp.Common
{
	/// <summary>
	/// Represents a JsonResult interaction to send information to the client
	/// </summary>
	public class GenericStateResult : JsonResult
	{
		public GenericStateResult(bool isError = false, string message = null, object innerData = null)
		{
			this.IsError = isError;
			this.Message = message;
			this.InnerData = innerData;
		}

		public bool IsError { get; set; }

		public string Message { get; set; }

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