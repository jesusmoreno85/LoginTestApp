using LoginTestApp.Business.Contracts;

namespace LoginTestApp.Repository.Contracts
{
	/// <summary>
	/// Represents a repository to some specific model type
	/// </summary>
	/// <typeparam name="TModel">The model type to work with</typeparam>
	/// <typeparam name="TKey">The primary key type of this model</typeparam>
	public interface IRepository<TModel, in TKey> : IRepository
        where TModel : IModel<TKey>
	{
		void Create(TModel model);

		TModel GetById(TKey id);

		void Delete(TModel model);

		int DeleteById(TKey id);

		void Update(TModel model);
	}

    public interface IRepository { }
}