using System.Web.Http;

namespace SampleHost
{
    public class HelloController : ApiController
    {
        [Route("hello")]
        public string Get()
        {
            return "hello!";
        }
    }
}