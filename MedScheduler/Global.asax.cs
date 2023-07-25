using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;

namespace MedScheduler
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            // Specify the file path for the log file
            string logFilePath = HttpContext.Current.Server.MapPath("~/errorLOG.txt");

            // Use using statement to properly handle the file stream and writer
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine("ERROR DATE: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine("ERROR MESSAGE: " + ex.Message);
                writer.WriteLine("SOURCE: " + ex.Source);
                writer.WriteLine("FORM NAME: " + HttpContext.Current.Request.Url.ToString());
                writer.WriteLine("QUERYSTRING: " + HttpContext.Current.Request.QueryString.ToString());
                writer.WriteLine("TARGETSITE: " + ex.TargetSite.ToString());
                writer.WriteLine("STACKTRACE: " + ex.StackTrace);
                writer.WriteLine("-------------------------------------------------------------------------------------------------------------");
            }
        }
    }
}
