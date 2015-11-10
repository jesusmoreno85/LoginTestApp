using System;
using LoginTestApp.Business.Contracts;
using LoginTestApp.DataAccess.Contracts;

namespace LoginTestApp.Repository
{
    internal interface IDataInteractions
    {
        event Action<IModel, IEntity> OnDataChange;
    }
}