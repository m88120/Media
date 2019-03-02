using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AllYouMedia
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string CurrencySymbol = "$";
        public static string Currency = "USD";
        public static int BuldVersion = 7;
        protected void Application_BeginRequest()
        {
			
            //if (Request.IsSecureConnection == false && Request.IsLocal == false)
            //{
            //    Response.Redirect(Request.Url.AbsoluteUri.Replace("http://www.", "https://www.").Replace("http://", "https://"));
            //}
			
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AllYouMedia.DataAccess.InfrastructureLayer.DependencyConfigure.Initialize();
        }

    }
}
