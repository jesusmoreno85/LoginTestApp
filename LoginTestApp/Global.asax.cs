using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.Mvc;
using LoginTestApp.App_Start;
using LoginTestApp.Crosscutting;
using LoginTestApp.DataAccess.Context;

namespace LoginTestApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new LoginTestAppInitializer());
            //Database.SetInitializer<LoginTestAppContext>(null);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //We should remove this, the validation must me manual and controlled by business
            FluentValidationModelValidatorProvider.Configure();

            Bootstrapper.Initialise();
        }
    }
}
