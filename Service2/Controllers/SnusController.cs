using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SnusController : ControllerBase
    {
        public SnusController() { }

        [HttpGet]
        public string GetHello()
        {
            return "hello from service2";
        }
    }
}
