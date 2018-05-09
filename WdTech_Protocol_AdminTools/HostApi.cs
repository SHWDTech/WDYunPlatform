using System;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;

namespace WdTech_Protocol_AdminTools
{
    public class HostApi
    {
        public static void StartHost()
        {
            var config = new HttpSelfHostConfiguration($"http://localhost:9090");

            var assembly = Assembly.GetExecutingAssembly().Location;
            var path = assembly.Substring(0, assembly.LastIndexOf("\\", StringComparison.Ordinal)) + "\\WdTech_Protocol_Api.dll";
            config.Services.Replace(typeof(IAssembliesResolver), new SelfHostAssemblyResolver(path));

            // Web API configuration and services
            var authenticationName = ConfigurationManager.AppSettings["AuthName"];
            if (authenticationName == null) throw new ArgumentException("lost application setting AuthName");

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("API default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }
    }
}
