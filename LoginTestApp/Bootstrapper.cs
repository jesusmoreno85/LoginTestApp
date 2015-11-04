using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using LoginTestApp.Business.Contracts.Managers;
using LoginTestApp.Business.Contracts.Strategies;
using LoginTestApp.Business.Managers;
using LoginTestApp.Business.Strategies;
using LoginTestApp.Controllers;
using LoginTestApp.Crosscutting;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Crosscutting.Contracts.Email;
using LoginTestApp.Crosscutting.EMail;
using LoginTestApp.Crosscutting.EMail.Templates;
using LoginTestApp.DataAccess.Context;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.Repository;
using LoginTestApp.Repository.Contracts;
using LoginTestApp.Repository.MappingConfiguration;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using IConfigurationProvider = LoginTestApp.Crosscutting.Contracts.IConfigurationProvider;

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

			RegisterCrosscuttingConcerns(container);
			RegisterStrategies(container);
			RegisterManagers(container);
			RegisterControllers(container);

			//Injection Constructions
			container.RegisterType<ILoginTestAppContext, LoginTestAppContext>
				(new InjectionConstructor("LoginTestAppContext", new ResolvedParameter(typeof(ISystemContext))));

			return container;
		}

		private static void RegisterStrategies(UnityContainer container)
		{
			container.RegisterType<IPasswordRecoveryStrategy, PasswordRecoveryStrategy>();
		}

		private static void RegisterControllers(IUnityContainer container)
		{
			container.RegisterType<IController, LoginController>();
		}

		private static void RegisterManagers(IUnityContainer container)
		{
			container.RegisterType<IAccountManager, AccountManager>();
			container.RegisterType<IRepositoryManager, RepositoryManager>();
		}

		private static void RegisterCrosscuttingConcerns(IUnityContainer container)
		{
			container.RegisterType<ILogger, Logger>(new InjectionConstructor("LoginTestApp", "Application"));
			container.RegisterType<ISystemContext, HttpContext>(new InjectionConstructor("App Full Name"));
			container.RegisterType<ICryptoProvider, CryptoProvider>();

			container.RegisterType<IConfigurationProvider, ConfigurationProvider>();

			var configProvider = container.Resolve<IConfigurationProvider>();

			var host = configProvider.GetSectionKeyValue("SmtpEmailSender", "Host");
			var port = configProvider.GetSectionKeyValue<int>("SmtpEmailSender", "Port");
			var userName = configProvider.GetSectionKeyValue("SmtpEmailSender", "UserName");
			var userPassword = configProvider.GetSectionKeyValue("SmtpEmailSender", "UserPassword");

			container.RegisterType<IEmailSender, EmailSender>(
				new InjectionConstructor(host, port, userName, userPassword, container.Resolve<ICryptoProvider>())); 
				
			IEnumerable<Profile> configProfiles = new[] { (new LoginTestAppConfigProfile()) as Profile };
			container.RegisterType<IDataMapper, DataMapper>(new InjectionConstructor(configProfiles));
		}
	}
}