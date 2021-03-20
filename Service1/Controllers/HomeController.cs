using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

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
    }
}