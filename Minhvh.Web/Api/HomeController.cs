using Minhvh.Service;
using Minhvh.Web.Infrastructure.Core;
using System.Web.Http;

namespace Minhvh.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        private IErrorCodeService _errorCodeService;

        public HomeController(IErrorCodeService errorCodeService) : base(errorCodeService)
        {
            this._errorCodeService = errorCodeService;
        }

        [HttpGet]
        [Route("TestMethod")]
        [System.Web.Http.AcceptVerbs("Get")]
        public string TestMethod()
        {
            return "Hello, TEDU Member. ";
        }
    }
}