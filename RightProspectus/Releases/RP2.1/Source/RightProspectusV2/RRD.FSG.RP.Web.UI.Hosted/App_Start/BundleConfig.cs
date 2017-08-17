// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using System.Web.Optimization;

/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
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
                        "~/Scripts/jquery-{version}.js",new CssRewriteUrlTransform()));


            bundles.Add(new ScriptBundle("~/Scripts/MinJS").Include(
                     
                      "~/Scripts/jquery-{version}.js",new CssRewriteUrlTransform()).Include(
                      "~/Scripts/jquery-ui-{version}.min.js", new CssRewriteUrlTransform()
                      ));

            bundles.Add(new ScriptBundle("~/Scripts/HostedEngine/RequestMaterialDialogBoxMinJS").Include(
                        "~/Scripts/HostedEngine/RequestMaterial.min.js", new CssRewriteUrlTransform()
                        ));

            bundles.Add(new StyleBundle("~/Content/RequestMaterialDialogBoxCSS").Include(
                "~/Content/RP/RequestMaterial.css",new CssRewriteUrlTransform()).Include(
                "~/Content/RP/JqueryDialogBox.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/themes/base/RequestMaterialDialogBoxThemesCSS").Include(
                "~/Content/themes/base/dialog.css", new CssRewriteUrlTransform()).Include(
                "~/Content/themes/base/theme.css", new CssRewriteUrlTransform()).Include(
                "~/Content/themes/base/button.css", new CssRewriteUrlTransform()).Include(
                "~/Content/themes/base/core.css", new CssRewriteUrlTransform()));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}