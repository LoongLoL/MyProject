using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 简单手机号码验证.Common;

namespace 简单手机号码验证.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        PhoneValidator validator = null;
        public HomeController(PhoneValidator pv)
        {
            validator = pv;
        }

        [HttpGet("login")]
        public IActionResult Login(string phone)
        {
            bool accept = validator.IsPhone(ref phone);
            return new JsonResult(new { phone, accept });
        }
    }
}