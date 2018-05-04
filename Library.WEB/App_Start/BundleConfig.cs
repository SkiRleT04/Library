using System.Web;
using System.Web.Optimization;

namespace Library.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                    "~/Scripts/jquery-{version}.js",
                                    "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));




            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/themes/base/jquery-ui.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/justified-nav.css"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
            "~/Scripts/Custom/popper.min.js",
            "~/Scripts/Custom/custom.js",
            "~/Scripts/Custom/jquery-3.2.1.slim.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                "~/Scripts/Login/ie-emulation-modes-warning.js",
                "~/Scripts/Login/ie10-viewport-bug-workaround.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/Admin/ie-emulation-modes-warning.js",
                "~/Scripts/Admin/jquery.min.js",
                "~/Scripts/Admin/bootstrap.min.js",
                "~/Scripts/Admin/holder.min.js",
                "~/Scripts/Admin/ie10-viewport-bug-workaround.js"
                ));

            bundles.Add(new StyleBundle("~/Content/Login/css").Include(
                "~/Content/Login/bootstrap.min.css",
                "~/Content/Login/ie10-viewport-bug-workaround.css",
                "~/Content/Login/signin.css"
                ));


            bundles.Add(new StyleBundle("~/Content/Admin/css").Include(
                "~/Content/Admin/bootstrap.min.css",
                "~/Content/Admin/ie10-viewport-bug-workaround.css",
                "~/Content/Admin/dashboard.css"
                ));

            bundles.Add(new ScriptBundle("~/Kendo").Include("~/Kendo/js/kendo.all.min.js",
               "~/Kendo/js/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Kendo/styles").Include("~/Kendo/styles/kendo.common.min.css",
                "~/Kendo/styles/kendo.default.min.css"));
        }
    }
}
