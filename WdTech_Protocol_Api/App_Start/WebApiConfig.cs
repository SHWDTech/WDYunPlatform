using System;
using System.Configuration;
using System.Web.Http;

namespace WdTech_Protocol_Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var authenticationName = ConfigurationManager.AppSettings["AuthName"];
            if (authenticationName == null) throw new ArgumentException("lost application setting AuthName");
            config.MessageHandlers.Add(new HmacAutheResponseDelegateHandler(300, "cpx",
                new ChargingPileAllowedAppProvider("cpx")));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}