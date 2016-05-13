using System.Web.Http;
using System.Web.Http.Controllers;

namespace Strathweb.WebApi.IpFiltering
{
    public class IpFilterAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return actionContext.Request.IsIpAllowed();
        }
    }
}