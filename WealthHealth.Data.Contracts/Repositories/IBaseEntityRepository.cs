using System;
using System.Linq;

namespace WealthHealth.Data.Contracts.Repositories
{
    public interface IBaseEntityRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();

        T Find(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        IDbOpResult ExecuteSqlCommand(string command, params Object[] parameters);
    }
}