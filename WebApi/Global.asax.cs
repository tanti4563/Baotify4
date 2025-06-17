using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                UnityConfig.RegisterComponents();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Application_Start Error: {ex.Message}");
                throw; // Re-throw to see the error
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            System.Diagnostics.Debug.WriteLine($"Application Error: {exception?.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {exception?.StackTrace}");
        }

    }
}
