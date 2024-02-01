using System.Web;
using System.Web.Optimization;

namespace SIMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                 "~/Scripts/Jquery/jquery-{version}.js",
                 "~/Scripts/umd/popper.min.js",
                 "~/Scripts/Bootstrap/bootstrap.bundle.min.js",
                 "~/Scripts/Jquery/jquery.easing.min.js",
                 "~/Scripts/Jquery/jquery.sticky.js",
                 "~/Scripts/Admin/isotope.pkgd.min.js",
                 "~/Scripts/Admin/venobox.min.js",
                 "~/Scripts/Admin/main.js",
                 "~/Scripts/Plugins/toastr.js"
                 
                         ));
            bundles.Add(new ScriptBundle("~/bundles/adminjquery").Include(
                 "~/Scripts/Jquery/jquery-{version}.js",
                 "~/Scripts/umd/popper.min.js",
                 "~/Scripts/Bootstrap/bootstrap.bundle.min.js",
                 "~/Scripts/Jquery/jquery-ui.js",
                 "~/Scripts/Jquery/jquery.easing.min.js",
                 "~/Scripts/Jquery/jquery.sticky.js",
                 "~/Scripts/Admin/isotope.pkgd.min.js",
                 "~/Scripts/venobox.min.js",
                 "~/Scripts/Admin/mainAdmin.js",
                 "~/Scripts/Plugins/toastr.js",
                 "~/Scripts/Admin/app.js",
                 "~/Scripts/Admin/dashboard.js"
                 
                ));
           

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Bootstrap/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Bootstrap/bootstrap.css",
                      "~/Content/Jquery/jquery-ui.min.css",
                      "~/Content/Admin/site.css",
                      "~/Content/fonts/font-awesome.css",
                      "~/Content/Admin/boxicons.min.css",
                      "~/Content/Admin/animate.min.css",
                      "~/Content/Admin/venobox.css",
                      "~/Content/Admin/owl.carousel.min.css",
                      "~/Content/Admin/style.css",
                      "~/Content/Admin/toastr.css"

                       ));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                      "~/Content/Bootstrap/bootstrap.css",
                      "~/Content/Jquery/jquery-ui.min.css",
                      "~/Content/Jquery/jquery-ui.theme.css",
                      "~/Content/fonts/font-awesome.css",
                      "~/Content/Admin/perfect-scrollbar.css",
                      "~/Content/Admin/app.css",
                      "~/Content/Admin/toastr.css",
                      "~/Content/Admin/style.css",
                      "~/Content/Admin/admin.css",
                      "~/Content/Admin/venobox.css"
                      ));

        }
    }
}