using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using WealthHealth.Model.Core;

namespace WealthHealth.Data.Contracts.Repositories.Core
{
    public interface IApplicationUserRepository : IBaseEntityRepository<ApplicationUser>
    {
        Task<IdentityResult> RegisterUser(string userName, string email, string password);

        Task<IdentityUser> FindUser(string userName, string password);
    }
}