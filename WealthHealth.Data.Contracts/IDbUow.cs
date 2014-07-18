using System;
using WealthHealth.Data.Contracts.Repositories.Core;

namespace WealthHealth.Data.Contracts
{
    public interface IDbUow : IDisposable
    {
        // Repositories

        // Wealth / Investments
        IApplicationUserRepository ApplicationUsers { get; }

        void Commit();

        IDbOpResult Save();
    }
}