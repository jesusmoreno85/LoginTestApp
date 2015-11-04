
namespace LoginTestApp.Crosscutting.Contracts
{
    public interface IDataMapper
    {
        TDestination MapTo<TDestination>(object source);

        object MapTo(object source, object destination);
    }
}
