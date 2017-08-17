// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************



/// <summary>
/// The HostedAdmin namespace.
/// </summary>
using System.Web.Optimization;
namespace RRD.FSG.RP.Web.UI.HostedAdmin
{
    /// <summary>
    /// Class BundleConfig.
    /// </summary>
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                       "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/RP/site.css"));

            //bundles.Add(new StyleBundle("~/Content/Kendo").Include(
            //          "~/Content/Kendo/kendo*"));

            bundles.Add(new StyleBundle("~/Content/Kendo/css").Include(
                      "~/Content/Kendo/kendo.common.min.css",
                      "~/Content/Kendo/kendo.bootstrap.min.css",
                      "~/Content/Kendo/kendo.dataviz.min.css",
                      "~/Content/Kendo/kendo.dataviz.bootstrap.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/Kendo").Include(
                        "~/Scripts/Kendo/jquery.min.js",
                        "~/Scripts/Kendo/angular.min.js",
                        "~/Scripts/Kendo/kendo.all.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/VerticalDataImport").Include(
                       "~/Scripts/jquery-1.12.4.min.js",
                       "~/Scripts/jszip.min.js",
                       "~/Scripts/Kendo/kendo-latest.all.min.js"
                       ));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}