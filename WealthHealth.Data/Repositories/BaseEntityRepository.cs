using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using WealthHealth.Data.Contracts;

namespace WealthHealth.Data.Repositories
{
    using WealthHealth.Data.Contracts.Repositories;

    public class BaseEntityRepository<T> : IBaseEntityRepository<T>
        where T : class
    {
        public BaseEntityRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            DbContext = dbContext;
            this.DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public void Dispose()
        {
            if (DbContext != null)
            {
                DbContext.Dispose();
            }
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.DbSet;
        }

        public virtual T Find(int id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry<T> dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = this.Find(id);
            if (entity == null)
            {
                return; // not found; assume already deleted.
            }

            this.Delete(entity);
        }

        public IDbOpResult ExecuteSqlCommand(string command, params Object[] parameters)
        {
            if (command == null)
            {
                throw new ArgumentException("The stored procedure must be provided");
            }

            var opStatus = new DbOpResult { OperationSuccessStatus = false };

            try
            {
                opStatus.OperationSuccessStatus = DbContext.Database.ExecuteSqlCommand(command, parameters) > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOpResult.CreateFromException("Error executing Stored Procedure", ex);
            }

            return opStatus;
        }

        protected IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentException("Predicate expression must be provided");
            }

            return DbContext.Set<T>().Where(predicate);
        }

        protected IQueryable<T> GetOrderByAscending(System.Linq.Expressions.Expression<Func<T, string>> orderBy)
        {
            if (orderBy == null)
            {
                throw new ArgumentException("Predicate expression and Order By expression must be provided");
            }

            return DbContext.Set<T>().OrderBy(orderBy);
        }

        protected IQueryable<T> GetOrderByAscending(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, string>> orderBy)
        {
            if (predicate == null || orderBy == null)
            {
                throw new ArgumentException("Predicate expression and Order By expression must be provided");
            }

            return DbContext.Set<T>().Where(predicate).OrderBy(orderBy);
        }

        protected IQueryable<T> GetOrderByDescending(System.Linq.Expressions.Expression<Func<T, string>> orderBy)
        {
            if (orderBy == null)
            {
                throw new ArgumentException("Predicate expression and Order By expression must be provided");
            }

            return DbContext.Set<T>().OrderByDescending(orderBy);
        }

        protected IQueryable<T> GetOrderByDescending(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, string>> orderBy)
        {
            if (predicate == null || orderBy == null)
            {
                throw new ArgumentException("Predicate expression and Order By expression must be provided");
            }

            return DbContext.Set<T>().Where(predicate).OrderByDescending(orderBy);
        }

        protected IQueryable<T> GetQueryable()
        {
            return DbContext.Set<T>();
        }
    }
}