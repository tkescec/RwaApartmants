using System.Web;
using System.Web.Optimization;

namespace Listeo
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/jquery-3.4.1.min.js",
                        "~/Scripts/jquery-migrate-3.1.0.min.js",
                        "~/Scripts/mmenu.min.js",
                        "~/Scripts/chosen.min.js",
                        "~/Scripts/slick.min.js",
                        "~/Scripts/rangeslider.min.js",
                        "~/Scripts/magnific-popup.min.js",
                        "~/Scripts/waypoints.min.js",
                        "~/Scripts/counterup.min.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/tooltips.min.js",
                        "~/Scripts/custom.js",
                        "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/style.css",
                      "~/Content/main-color.css",
                      "~/Content/site.css"));
        }
    }
}
