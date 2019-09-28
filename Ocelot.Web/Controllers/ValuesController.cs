using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ocelot.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Web value1", "Web value2",Request.Host.Port.ToString() };
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Json()
        {
            return new string[] { "Web Json Data value1", "Web Json data", Request.Host.Port.ToString() };
        }
    }
}