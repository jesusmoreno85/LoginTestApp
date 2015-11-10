
namespace LoginTestApp.Business.Contracts
{
	public interface IModel<TKey> : IModel
    {
		TKey Id { get; set; }

        //Comment AngelM: We are not taking the administration properties in the model. CreatedBy, CreatedDate and so on.
	}

    public interface IModel
    {

    }
}
