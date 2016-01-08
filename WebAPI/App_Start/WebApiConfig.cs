using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "route/{controller}/{action}",
                defaults: new { action = RouteParameter.Optional }
            );

            config.Filters.Add(new ValidateModelAttribute());
        }
    }


}
