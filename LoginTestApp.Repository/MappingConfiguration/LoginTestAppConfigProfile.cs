using AutoMapper;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Repository.ExtensionMethods;
using Entities = LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.Repository.MappingConfiguration
{
	public class LoginTestAppConfigProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<User, Entities.User>()
				.IgnoreAllNonExisting();

			CreateMap<Entities.User, User>()
				.IgnoreAllNonExisting();

			CreateMap<DynamicLink, Entities.DynamicLink>()
				.IgnoreAllNonExisting();

			CreateMap<Entities.DynamicLink, DynamicLink>()
				.IgnoreAllNonExisting();

            CreateMap<Entities.Module, Module>()
                .IgnoreAllNonExisting();

            CreateMap<Module, Entities.Module>()
                .IgnoreAllNonExisting();

            CreateMap<Entities.ModuleAction, ModuleAction>()
                .IgnoreAllNonExisting();

            CreateMap<ModuleAction, Entities.ModuleAction>()
                .IgnoreAllNonExisting();

        }
	}
}
