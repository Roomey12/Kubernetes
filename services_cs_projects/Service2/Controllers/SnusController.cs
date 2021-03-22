using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SnusController : ControllerBase
    {
        private readonly IEnumerable<string> _service1Ports = new List<string>();
        private readonly IServiceProvider _serviceProvider;
        static readonly HttpClient client = new HttpClient();

        public SnusController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public string GetHello()
        {
            return "hello from service2";
        }


        [HttpGet]
        [Route("GetError")]
        public IActionResult CreateError()
        {
            throw new Exception("this is exception");
        }

        [HttpGet]
        [Route("CallServiceDelayedEndpoint")]
        public async Task<IActionResult> CallService1([FromQuery] int amountOfCallsToService1 = 10,
            [FromQuery] int delayForEachRequest = 1000)
        {
            try
            {
                var tasks = new List<Task>();

                for (int i = 0; i < amountOfCallsToService1; i++)
                {
                    tasks.Add(client.GetAsync(
                        $"http://service1-service:8080/Home/TimeOutRequest?timeout={delayForEachRequest}"));
                }

                await Task.WhenAll(tasks);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}