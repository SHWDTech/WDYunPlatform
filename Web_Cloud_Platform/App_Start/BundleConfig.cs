using System.Web.Optimization;

namespace Web_Cloud_Platform
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

            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                        "~/Scripts/Base/const.js",
                        "~/Scripts/Base/base.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Utility/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Utility/bootstrap.js",
                      "~/Scripts/Utility/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                "~/Scripts/Utility/moment-with-locales.js",
                "~/Scripts/Utility/bootstrap-datetimepicker.js"));


            //下面开始是CSS的bundler
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/base/site.css"));

            bundles.Add(new StyleBundle("~/Content/datetimepickercss").Include(
                "~/Content/bootstrap/bootstrap-datetimepicker.css"));
        }
    }
}
