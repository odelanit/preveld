using System.Web.Optimization;

namespace Preveld
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/printThis").Include(
                "~/Scripts/printThis.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/jquery-ui.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/plugins/fontawesome-pro/css/all.min.css",
                      "~/Content/adminlte.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                "~/Content/plugins/fontawesome-pro/css/all.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/adminlte/js").Include("~/Scripts/adminlte.min.js"));
        }
    }
}
