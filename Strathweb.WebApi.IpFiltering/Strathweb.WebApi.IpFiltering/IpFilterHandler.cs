using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

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
}