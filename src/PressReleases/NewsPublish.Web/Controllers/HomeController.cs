using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPublish.Web.Models;
using NewsPublish.Service;

namespace NewsPublish.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsService _newsSerrvice;
        private readonly BannerService _bannerService;

        public HomeController(ILogger<HomeController> logger, NewsService newsSerrvice, BannerService bannerService)
        {
            _logger = logger;
            _newsSerrvice = newsSerrvice;
            _bannerService = bannerService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "首页";
            var newsClassifies = _newsSerrvice.GetNewsClassifyList();
            return View(newsClassifies);
        }
        [HttpGet]
        public JsonResult GetBanner()
        {
            return Json(_bannerService.GetBannerList());
        }
    }
}
