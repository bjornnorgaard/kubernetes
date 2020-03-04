using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Person;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EnvironmentController : ControllerBase
    {
        private readonly ILogger<EnvironmentController> _logger;
        private static bool _isReady = true;
        private static bool _isAlive = true;
        private static DateTime _isReadyTimestamp;

        public EnvironmentController(ILogger<EnvironmentController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("{key}")]
        public ActionResult<string> Get(string key)
        {
            var value = Environment.GetEnvironmentVariable(key);
            return value;
        }

        [HttpGet("health")]
        public ActionResult GetHealth()
        {
            var health = new
            {
                Ready = _isReady,
                Alive = _isAlive
            };
            return Ok(health);
        }

        [HttpGet("ready")]
        public ActionResult GetReady()
        {
            var span = DateTime.Now.Subtract(_isReadyTimestamp);
            if (span > new TimeSpan(0, 2, 0)) _isReady = true;
            if (_isReady) return Ok("Ready");
            return BadRequest("Not ready");
        }

        [HttpPost("not-ready")]
        public ActionResult SetReady()
        {
            _isReady = false;
            _isReadyTimestamp = DateTime.Now;
            return Ok("Not ready");
        }

        [HttpGet("alive")]
        public ActionResult GetAlive()
        {
            if (_isAlive) return Ok();
            
            while (true)
            { 
                Thread.Sleep(5000);
            }
        }

        [HttpPost("kill")]
        public ActionResult Kill()
        {
            _isAlive = false;
            _isReady = false;

            _logger.LogInformation("I was killed");

            return Ok("Killed");
        }
    }
}
