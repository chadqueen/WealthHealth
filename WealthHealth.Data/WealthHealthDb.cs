using Microsoft.AspNet.Identity.EntityFramework;
using WealthHealth.Model.Core;

namespace WealthHealth.Data
{
    public class WealthHealthDb : IdentityDbContext<ApplicationUser>
    {
        public WealthHealthDb()
            : base("WealthHealth", throwIfV1Schema: false)
        {
        }

        public static WealthHealthDb Create()
        {
            return new WealthHealthDb();
        }
    }
}