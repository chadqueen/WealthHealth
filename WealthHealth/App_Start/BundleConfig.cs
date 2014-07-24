using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using System.Web.Optimization;

namespace WealthHealth
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // general config
            bundles.UseCdn = false;

            // bundles by type
            RegisterJs(bundles);
            RegisterCss(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterJs(BundleCollection bundles)
        {
            var scriptTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();
            var nullBuilder = new NullBuilder();

            // jQuery
            var jqueryBundle = new Bundle("~/bundles/js/jquery")
                .Include("~/Scripts/jquery-{version}.js");
            jqueryBundle.Builder = nullBuilder;
            jqueryBundle.Transforms.Add(scriptTransformer);
            jqueryBundle.Orderer = nullOrderer;
            bundles.Add(jqueryBundle);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            var modernizrBundle = new Bundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*");
            modernizrBundle.Builder = nullBuilder;
            modernizrBundle.Transforms.Add(scriptTransformer);
            modernizrBundle.Orderer = nullOrderer;
            bundles.Add(modernizrBundle);

            // 3rd Party JavaScript files
            var vendorBundle = new Bundle("~/bundles/js/vendor")
                .Include(
                    "~/Scripts/angular.js",
                    "~/Scripts/angular-route.js",
                    "~/Scripts/angular-animate.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/toastr.min.js",
                    "~/Scripts/underscore.js");
            vendorBundle.Builder = nullBuilder;
            vendorBundle.Transforms.Add(scriptTransformer);
            vendorBundle.Orderer = nullOrderer;
            bundles.Add(vendorBundle);

            // WealthHealth Angular App
            var wealthHealthAppBundle = new Bundle("~/bundles/js/wealthhealth")
                // Notify Component
                .Include(
                    "~/app/components/services/notify/notify.js",
                    "~/app/components/services/notify/notifyService.js")

                // Route Resolver
                .Include(
                    "~/app/components/providers/routeResolver/routeResolver.js",
                    "~/app/components/providers/routeResolver/routeResolverProvider.js")

                // Authentication
                .Include("~/app/authentication/authentication.js")
                .IncludeDirectory("~/app/authentication/controllers", "*Ctrl.js", true)
                .Include("~/app/authentication/authenticationRoutes.js")

                // Core
                .Include("~/app/WealthHealth.js")

                // Sections
                .IncludeDirectory("~/app/sections", "*Ctrl.js", true)

                // Config
                .Include("~/app/config/wealthHealthRoutes.js");

                
            wealthHealthAppBundle.Builder = nullBuilder;
            wealthHealthAppBundle.Transforms.Add(scriptTransformer);
            wealthHealthAppBundle.Orderer = nullOrderer;
            bundles.Add(wealthHealthAppBundle);
        }

        private static void RegisterCss(BundleCollection bundles)
        {
            var styleTransformer = new StyleTransformer();
            var cssMinify = new CssMinify();
            var nullOrderer = new NullOrderer();
            var nullBuilder = new NullBuilder();

            var appStyleBundle = new Bundle("~/bundles/css/app")
                .Include(
                    "~/Content/less/build.less",
                    "~/Content/sass/site.scss");
            appStyleBundle.Builder = nullBuilder;
            appStyleBundle.Transforms.Add(styleTransformer);
            appStyleBundle.Transforms.Add(cssMinify);
            appStyleBundle.Orderer = nullOrderer;
            bundles.Add(appStyleBundle);
        }
    }
}
