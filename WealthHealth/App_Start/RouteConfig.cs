using System.Web.Mvc;
using System.Web.Routing;

namespace WealthHealth
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Set route for WealthHealth Angular App
            routes.MapRoute(
                name: "WealthHealthAngularApp",
                url: "wealthhealth/{*pathInfo}",
                defaults: new { controller = "WealthHealth", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
