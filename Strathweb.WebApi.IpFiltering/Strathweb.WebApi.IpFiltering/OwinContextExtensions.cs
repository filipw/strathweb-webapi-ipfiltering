using System.Configuration;
using System.Linq;
using Microsoft.Owin;
using Strathweb.WebApi.IpFiltering.Configuration;

namespace Strathweb.WebApi.IpFiltering
{
    public static class OwinContextExtensions
    {
        public static bool IsIpAllowed(this IOwinContext ctx)
        {
            if (ctx.Request.LocalIpAddress == ctx.Request.RemoteIpAddress) return true;

            var ipAddress = ctx.Request.RemoteIpAddress;
            var ipFiltering = ConfigurationManager.GetSection("ipFiltering") as IpFilteringSection;
            if (ipFiltering != null && ipFiltering.IpAddresses != null && ipFiltering.IpAddresses.Count > 0)
            {
                if (ipFiltering.IpAddresses.Cast<IpAddressElement>().Any(ip => (ipAddress == ip.Address && !ip.Denied)))
                {
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}