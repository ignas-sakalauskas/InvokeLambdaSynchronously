using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LambdaAspNetCoreServerAlb.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }
    }
}
