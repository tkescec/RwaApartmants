using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Administrator
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/App").Include(
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
                            "~/Scripts/custom.js"));
        }
    }
}