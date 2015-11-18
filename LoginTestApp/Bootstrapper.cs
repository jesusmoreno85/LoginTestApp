using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using LoginTestApp.Business.Contracts.Managers;
using LoginTestApp.Business.Contracts.ModelValidators;
using LoginTestApp.Business.Contracts.Strategies;
using LoginTestApp.Business.Managers;
using LoginTestApp.Business.ModelValidators;
using LoginTestApp.Business.Strategies;
using LoginTestApp.Controllers;
using LoginTestApp.Crosscutting;
using LoginTestApp.Crosscutting.Contracts;
using LoginTestApp.Crosscutting.Contracts.Email;
using LoginTestApp.Crosscutting.EMail;
using LoginTestApp.DataAccess.Context;
using LoginTestApp.DataAccess.Contracts.Context;
using LoginTestApp.Repository;
using LoginTestApp.Repository.Contracts;
using LoginTestApp.Repository.Contracts.Repositories;
using LoginTestApp.Repository.MappingConfiguration;
using LoginTestApp.Repository.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc3;
using DependencyResolver = System.Web.Mvc.DependencyResolver;
using IConfigurationProvider = LoginTestApp.Crosscutting.Contracts.IConfigurationProvider;
using IDependencyResolver = LoginTestApp.Crosscutting.Contracts.IDependencyResolver;

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

            container.RegisterType<IDependencyResolver, Crosscutting.DependencyResolver>(new InjectionConstructor(container));

            //TODO(AngelM): Move this configurations to another place
            RegisterCrosscuttingConcerns(container);

			return container;


            container.RegisterType<IController, AccountController>();
        }


        private static void RegisterCrosscuttingConcerns(IUnityContainer container)
		{
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

