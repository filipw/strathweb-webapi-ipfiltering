using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Strathweb.WebApi.IpFiltering
{
    public class IpFilterHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.IsIpAllowed())
            {
                return await base.SendAsync(request, cancellationToken);
            }

            return request.CreateErrorResponse(HttpStatusCode.Forbidden, "Cannot view this resource");
        }
    }

    public class IpFilterMiddleware : OwinMiddleware
    {
        public IpFilterMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            // if no identity present, continue - other components will reject this request anyway
            var principal = context.Authentication.User;
            if (principal?.Identity == null || !principal.Identity.IsAuthenticated)
            {
                await Next.Invoke(context);
                return;
            }

            // if identifier doesn't match any agent or customer, nullify the principal
            if (!principal.IsManager() && !UserExists(principal))
            {
                context.Authentication.User = new ClaimsPrincipal();
            }

            await Next.Invoke(context);
        }
    }
}