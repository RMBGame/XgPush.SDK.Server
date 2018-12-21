using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using XgPush.SDK.Server;

namespace Sample.XgPush.SDK.Server
{
    public class WebApiApplication : HttpApplication
    {
        private HttpClient GlobalHttpClient { get; set; }

        protected void Application_Start()
        {
            GlobalHttpClient = new HttpClient();

            XingePushClient.InitHttpClientCompatDefaultMagic();
            XgPushConfig.Register(GlobalHttpClient);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var removeList = new List<MediaTypeFormatter>();
            foreach (var formatter in GlobalConfiguration.Configuration.Formatters)
                if (!(formatter is JsonMediaTypeFormatter))
                    removeList.Add(formatter);
            foreach (var formatter in removeList) GlobalConfiguration.Configuration.Formatters.Remove(formatter);

            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.Headers.Remove("Server");
            HttpContext.Current.Response.Headers["Server"] = "Apache-Coyote/1.1";
        }

        private void Application_End(object sender, EventArgs e)
        {
            GlobalHttpClient?.Dispose();
        }
    }
}