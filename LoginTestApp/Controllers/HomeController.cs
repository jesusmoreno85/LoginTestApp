using System.Web.Mvc;

namespace LoginTestApp.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/
		public ActionResult Index()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return View(@"~/Views/Shared/Login2.cshtml", null);    
			}
			
			return View();
		}
	}
}