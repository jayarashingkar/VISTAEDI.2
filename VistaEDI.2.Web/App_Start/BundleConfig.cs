using System.Web.Optimization;
//
//namespace VistaEDI._2.Web.App_Start
namespace VistaEDI._2.Web.App_Start
{
      public class BundleConfig
        {
            // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
            public static void RegisterBundles(BundleCollection bundles)
            {
                bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-{version}.js"));


                bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                          "~/Scripts/bootstrap.min.js",
                          "~/Scripts/fuelux.min.js",
                          "~/Scripts/bootstrapValidator.min.js",
                          "~/Scripts/bootbox.min.js",
                          "~/Scripts/bootstrap-select.min.js",
                          "~/Scripts/bootstrap-datepicker.min.js"
                          ));

                bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap.min.css",
                          "~/Content/font-awesome.min.css",
                          "~/Content/fuelux.min.css",
                          "~/Content/bootstrapValidator.min.css",
                          "~/Content/bootstrap-select.min.css",
                          "~/Content/bootstrap-datepicker.min.css",
                          "~/Content/navmenu.css",
                          "~/Content/site.css"));

                bundles.Add(new ScriptBundle("~/bundles/Utilities").Include(
                          "~/Scripts/Utilities.js"
                          ));

                bundles.Add(new ScriptBundle("~/bundles/GridUtil").Include(
                          "~/Scripts/GridUtil.js"
                          ));

                bundles.Add(new ScriptBundle("~/bundles/DeviationList").Include(
                           "~/Scripts/pages/DeviationList.js"
                           ));

                //bundles.Add(new ScriptBundle("~/bundles/Index").Include(
                //           "~/Scripts/pages/Index.js"
                //           ));
            //bundles.Add(new ScriptBundle("~/bundles/ImportData").Include(
            //           "~/Scripts/pages/ImportData.js"
            //           ));

            BundleTable.EnableOptimizations = false;
            }
        } 
}