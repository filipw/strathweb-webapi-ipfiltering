using System.Configuration;
using System.Linq;
using System.Net.Http;
using Strathweb.WebApi.IpFiltering.Configuration;

namespace Strathweb.WebApi.IpFiltering
{
    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static bool IsIpAllowed(this HttpRequestMessage request)
        {
            if (!request.GetRequestContext().IsLocal)
            {
                var ipAddress = request.GetClientIpAddress();
                var ipFiltering = ConfigurationManager.GetSection("ipFiltering") as IpFilteringSection;
                if (ipFiltering != null && ipFiltering.IpAddresses != null && ipFiltering.IpAddresses.Count > 0)
                {
                    if (ipFiltering.IpAddresses.Cast<IpAddressElement>().Any(ip => (ipAddress == ip.Address && !ip.Denied)))
                    {
                        return true;
                    }

                    return false;
                }
            }

            return true;
        }

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            //Web-hosting
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }
            //Self-hosting
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }
            //Owin-hosting
            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic ctx = request.Properties[OwinContext];
                if (ctx != null)
                {
                    return ctx.Request.RemoteIpAddress;
                }
            }
            return null;
        }
    }
}