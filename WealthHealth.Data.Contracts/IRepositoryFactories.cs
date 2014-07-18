using System;
using System.Data.Entity;

namespace WealthHealth.Data.Contracts
{
    public interface IRepositoryFactories
    {
        Func<DbContext, object> GetRepositoryFactory<T>();

        Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class;
    }
}