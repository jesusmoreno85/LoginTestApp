using System.Web.Mvc;

namespace LoginTestApp.Controllers
{
    public class ControllerBase : Controller
    {
        protected internal JsonResult GetJson(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}