using System.Threading.Tasks;
using Microsoft.Owin;

namespace Strathweb.WebApi.IpFiltering
{
    public class IpFilterMiddleware : OwinMiddleware
    {
        public IpFilterMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (context.IsIpAllowed())
            {
                await Next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 403;
            }
        }
    }
}