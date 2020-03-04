using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 返回统一json格式.Models;

namespace 返回统一json格式.Controllers
{
    [Route("api/home"), ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            UserInfo info = new UserInfo()
            {
                Age = 22,
                Gender = true,
                Name = "Ron.lang",
                RegTime = null
            };
            return JsonReturn.成功.SetData("detail", info);
        }
    }
}
