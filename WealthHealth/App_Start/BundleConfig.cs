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

            var vendorBundle = new Bundle("~/bundles/js/vendor")
                .Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js");
            vendorBundle.Builder = nullBuilder;
            vendorBundle.Transforms.Add(scriptTransformer);
            vendorBundle.Orderer = nullOrderer;
            bundles.Add(vendorBundle);
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
