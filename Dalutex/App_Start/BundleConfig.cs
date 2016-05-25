using System.Web;
using System.Web.Optimization;

namespace Dalutex
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymaskmoney").Include(
                        "~/Scripts/jquery.maskmoney.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-select.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/equalheight").Include(
            "~/Scripts/jquery.equalheight.js"));

            bundles.Add(new ScriptBundle("~/bundles/prePageFunctions").Include(
            "~/Scripts/prePageFunctions.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
            "~/Scripts/bootstrap-datepicker*",
            "~/Scripts/locales/bootstrap-datepicker*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table").Include(
                    "~/Scripts/bootstrap-table.min.js",
                    "~/Scripts/bootstrap-table.js",
                    "~/Scripts/bootstrap-table-pt-BR.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-table").Include(
                      "~/Content/bootstrap-table.min.css",
                      "~/Content/bootstrap-table.css"));



            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                    "~/Scripts/fileinput.min.js",
                    "~/Scripts/fileinput.js",
                    "~/Scripts/fileinput_locale_es.js"));

            bundles.Add(new StyleBundle("~/Content/fileinput").Include(
                      "~/Content/fileinput.css",
                      "~/Content/fileinput.min.css"));


            bundles.Add(new StyleBundle("~/Content/datepicker").Include("~/Content/bootstrap-datepicker*"));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
