using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OhmValueCalculatorApp.App_Start
{
    public static class WebAPIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //ENABLE CORS FOR REACT JS APPLICATION            
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "OhmValue", id = RouteParameter.Optional }
            );
        }
    }
}