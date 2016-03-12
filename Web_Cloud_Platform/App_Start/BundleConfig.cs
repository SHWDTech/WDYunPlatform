using System.Web.Optimization;

namespace SHWDTech.Web_Cloud_Platform
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Utility/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Utility/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Utility/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/metroui").Include(
                      "~/Scripts/Utility/metro.js",
                      "~/Scripts/Utility/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Metro.UI.CSS/metro.css",
                      "~/Content/Metro.UI.CSS/metro-icons.css",
                      "~/Content/Metro.UI.CSS/metro-responsive.css",
                      "~/Content/Metro.UI.CSS/metro-rtl.css",
                      "~/Content/Metro.UI.CSS/metro-schemes.css",
                      "~/Content/Site.css"));
        }
    }
}
