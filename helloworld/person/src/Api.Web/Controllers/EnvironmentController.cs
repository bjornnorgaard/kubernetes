using System;
using System.Threading.Tasks;
using Application.Features.Person;
using Microsoft.AspNetCore.Mvc;

namespace Api.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EnvironmentController : ControllerBase
    {
        [HttpGet("{key}")]
        public ActionResult<string> Get(string key)
        {
            var value = Environment.GetEnvironmentVariable(key);
            return value;
        }
    }
}
