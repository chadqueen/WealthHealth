using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Threading.Tasks;
using WealthHealth.Data.Contracts.Repositories.Core;
using WealthHealth.Model.Core;

namespace WealthHealth.Data.Repositories.Core
{
    public class ApplicationUserRepository: BaseEntityRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserRepository(DbContext context) : base(context)
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)); ;
        }

        public async Task<IdentityResult> RegisterUser(string userName, string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email
            };

            var result = await this.userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);

            return user;
        }
    }
}