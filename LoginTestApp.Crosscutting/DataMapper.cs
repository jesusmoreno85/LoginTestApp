using System.Linq;
using AutoMapper;
using LoginTestApp.Crosscutting.Contracts;

namespace LoginTestApp.Crosscutting
{
    public class DataMapper : IDataMapper
    {
        public DataMapper(params Profile[] configProfiles)
        {
            //This will load all the configuration profiles in the Mapper static implementation
            Mapper.Initialize(cfg =>
            {
                configProfiles.ToList().ForEach(cfg.AddProfile);
            });

            Mapper.AssertConfigurationIsValid();
        }

        public TDestination MapTo<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}