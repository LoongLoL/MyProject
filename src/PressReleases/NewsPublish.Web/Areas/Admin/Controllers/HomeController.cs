using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewsPublish.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {
        // GET: Home
        public IActionResult Index()
        {
            return View();
        }
    }
}