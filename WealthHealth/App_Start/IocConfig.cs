using Ninject;
using System.Web.Http;
using System.Web.Mvc;
using WealthHealth.Services.DependencyInjection;

namespace WealthHealth.App_Start
{
    public class IocConfig
    {
        public static void RegisterIoc(HttpConfiguration config)
        {
            var kernel = new StandardKernel(); // Ninject IoC

            // Tell Mvc how to use our Ninject IoC
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            // Tell WebApi how to use our Ninject IoC (it uses a different resolver than Mvc);
            config.DependencyResolver = new NinjectDependencyResolver(kernel);
        }
    }
}