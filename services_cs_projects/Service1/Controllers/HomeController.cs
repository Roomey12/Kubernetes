using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public HomeController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        static readonly HttpClient client = new HttpClient();

        public IServiceProvider ServiceProvider { get; }

        [HttpGet]
        public string GetHello()
        {
            return "hello from service1";
        }


        [HttpGet]
        [Route("TimeOutRequest")]
        public string GetTimeOutResponse([FromQuery] int timeout = 0)
        {
            Thread.Sleep(timeout);
            return $"Response was sent for the {timeout} delay.";
        }

        [HttpGet]
        [Route("Ports")]
        public IActionResult GetPorts()
        {
            var server = ServiceProvider.GetRequiredService<IServer>();
            var addressFeature = server.Features.Get<IServerAddressesFeature>();
            return Ok(addressFeature.Addresses);
        }

        [HttpGet]
        [Route("CallService2")]
        public async Task<IActionResult> CallService1([FromQuery] string service2url)
        {
            if (string.IsNullOrEmpty(service2url)) return BadRequest("Empty path");

            var response = await client.GetAsync(service2url + "/Snus");
            return Ok($"Call successful. \n Result: {await response.Content.ReadAsStringAsync()}");
        }
    }
}