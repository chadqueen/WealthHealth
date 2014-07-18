using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WealthHealth.Startup))]
namespace WealthHealth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}