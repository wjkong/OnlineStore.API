using NLog;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private Logger logMgr = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            logMgr.Info("EStore API Application Start");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            logMgr.Info("EStore API Application End");
        }

        protected void Application_Error()
        {
            Exception lastException = Server.GetLastError();

            logMgr.Error(lastException);
        }
    }
}
