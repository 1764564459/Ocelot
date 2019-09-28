using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ocelot.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static int _count = 0;
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //_count++;
            //System.Console.WriteLine($"get...{_count}");
            //if (_count <= 3)
            //{
            //    Thread.Sleep(5000);
            //}
            return new string[] { "Api value1", "Api value2",Request.Host.Port.ToString() };
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Json()
        {
            _count++;
            System.Console.WriteLine($"get...{_count}");
            if (_count <= 3)
            {
                Thread.Sleep(5000);
            }
            return new string[] {"Json Action Result"};
        }
    }
}
