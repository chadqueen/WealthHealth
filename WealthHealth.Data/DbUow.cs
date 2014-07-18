using System;
using WealthHealth.Data.Contracts;
using WealthHealth.Data.Contracts.Repositories;

namespace WealthHealth.Data
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using WealthHealth.Data.Contracts.Repositories.Core;
    using WealthHealth.Model.Core;

    /// <summary>
    /// The "Unit of Work"
    ///     1) decouples the repos from the controllers
    ///     2) decouples the DbContext and EF from the controllers
    ///     3) manages the UoW
    /// </summary>
    /// <remarks>
    /// This class implements the "Unit of Work" pattern in which
    /// the "UoW" serves as a facade for querying and saving to the database.
    /// Querying is delegated to "repositories".
    /// Each repository serves as a container dedicated to a particular root entity type.
    /// A repository typically exposes "Get" methods for querying and
    /// will offer add, update, and delete methods if those features are supported.
    /// The repositories rely on their parent UoW to provide the interface to the
    /// data layer (which is the EF DbContext in Event Manager).
    /// </remarks>
    public class DbUow : IDbUow
    {
        public DbUow(IRepositoryProvider repositoryProvider)
        {
            this.RepositoryProvider = repositoryProvider;
            this.CreateContext();
        }

        // Core Repositories
        IApplicationUserRepository IDbUow.ApplicationUsers
        {
            get
            {
                return this.GetRepo<IApplicationUserRepository>();
            }
        }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        private WealthHealthDb DbContext { get; set; }

        private UserManager<ApplicationUser> UserManager { get; set; }

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            this.DbContext.SaveChanges();
        }

        /// <summary>
        /// Save pending changes to the database and return formatted operation result object
        /// </summary>
        public IDbOpResult Save()
        {
            var opStatus = new DbOpResult
            {
                OperationSuccessStatus = false,
                AffectedIndices = null
            };

            try
            {
                opStatus.OperationSuccessStatus = this.DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                opStatus = DbOpResult.CreateFromException("Error saving.", ex);
            }

            return opStatus;
        }

        #region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DbContext != null)
                {
                    this.DbContext.Dispose();
                }
            }
        }

        #endregion

        protected WealthHealthDb CreateContext()
        {
            this.DbContext = new WealthHealthDb();

            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));

            // Do NOT enable proxied entities, else serialization fails
            this.DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            this.DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            this.DbContext.Configuration.ValidateOnSaveEnabled = false;

            // DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.

            this.RepositoryProvider.DbContext = this.DbContext;

            return this.DbContext;
        }

        private IBaseEntityRepository<T> GetStandardRepo<T>() where T : class
        {
            return this.RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        private T GetRepo<T>() where T : class
        {
            return this.RepositoryProvider.GetRepository<T>();
        }
    }
}