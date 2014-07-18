using Ninject;
using System.Web.Http;
using System.Web.Mvc;
using WealthHealth.Data;
using WealthHealth.Data.Contracts;
using WealthHealth.Services.DependencyInjection;

namespace WealthHealth.App_Start
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using WealthHealth.Services.Auth;

    public class IocConfig
    {
        public static void RegisterIoc(HttpConfiguration config)
        {
            var kernel = new StandardKernel(); // Ninject IoC

            // These registrations are "per instance request".
            // See http://blog.bobcravens.com/2010/03/ninject-life-cycle-management-or-scoping/

            kernel.Bind<IRepositoryFactories>()
                .To<RepositoryFactories>().InSingletonScope();
            kernel.Bind<IRepositoryProvider>()
                .To<RepositoryProvider>()
                .WithConstructorArgument("repositoryFactories", context => context.Kernel.Get<IRepositoryFactories>());
            kernel.Bind<IDbUow>()
                .To<DbUow>()
                .WithConstructorArgument("repositoryProvider", context => context.Kernel.Get<IRepositoryProvider>());

            // Tell Mvc how to use our Ninject IoC
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            // Tell WebApi how to use our Ninject IoC (it uses a different resolver than Mvc);
            config.DependencyResolver = new NinjectDependencyResolver(kernel);
        }
    }
}