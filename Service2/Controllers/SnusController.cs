using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;

namespace Service1.Controllers
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
        [Route("Ports")]
        public IActionResult GetPorts()
        {
            var server = _serviceProvider.GetRequiredService<IServer>();
            var addressFeature = server.Features.Get<IServerAddressesFeature>();
            return Ok(addressFeature.Addresses);
        }

        [HttpGet]
        [Route("CallService1")]
        public async Task<IActionResult> CallService1([FromQuery] string service1url)
        {
            if (string.IsNullOrEmpty(service1url)) return BadRequest("Empty path");

            var response = await client.GetAsync(service1url + "/Home");
            return Ok($"Call successful. \n Result: {await response.Content.ReadAsStringAsync()}");
        }
    }
}