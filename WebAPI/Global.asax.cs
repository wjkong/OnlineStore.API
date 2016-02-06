using Kong.OnlineStoreAPI.Bootstrap;
using Kong.OnlineStoreAPI.Model;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static ILogMgr _logMgr;

        public static ILogMgr LogMgr
        {
            get { return _logMgr; }

        }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Initialise();

            //_logMgr = ServiceLocator.Current.GetInstance<ILogMgr>();

            //_logMgr.Info("Online Store API Application Starts");
        }

        protected void Application_End()
        {
            _logMgr.Info("Online Store API Application End");
        }

        protected void Application_Error()
        {
            Exception lastException = Server.GetLastError();

            _logMgr.Error(lastException);
        }
    }
}
