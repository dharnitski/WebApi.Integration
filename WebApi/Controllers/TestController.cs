using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Code;

namespace WebApi.Controllers
{
    [RoutePrefix("m/test")]
    public class TestController : ApiController
    {
        private readonly IDependency _dependency;

        public TestController(IDependency dependency)
        {
            _dependency = dependency;
        }

        [Route("ping")]
        [HttpGet]
        public string Ping()
        {
            var i = _dependency.GetSomething();

            return "pong";
        }

        [Route("secure")]
        [HttpGet]
        [Authorize]
        public string Secure()
        {
            return "secret";
        }
    }
}
