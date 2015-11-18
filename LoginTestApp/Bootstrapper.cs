using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc3;
using DependencyResolver = System.Web.Mvc.DependencyResolver;

namespace LoginTestApp
{
	public static class Bootstrapper
	{
		public static void Initialise()
		{
			var container = BuildUnityContainer();

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var container = new UnityContainer();
		    container.LoadConfiguration();

            return container;
        }
	}
}

