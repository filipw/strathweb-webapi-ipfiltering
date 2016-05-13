using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using SampleHost;
using Strathweb.WebApi.IpFiltering;

[assembly: OwinStartup(typeof(Startup))]
namespace SampleHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MessageHandlers.Add(new IpFilterHandler());
            config.MapHttpAttributeRoutes();

            app.Use<IpFilterMiddleware>();
            app.UseWebApi(config);
        }
    }
}