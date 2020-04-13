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
        private readonly NewsService _newsService;
        private readonly BannerService _bannerService;

        public HomeController(ILogger<HomeController> logger, NewsService newsService, BannerService bannerService)
        {
            _logger = logger;
            _newsService = newsService;
            _bannerService = bannerService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "首页";
            var newsClassifies = _newsService.GetNewsClassifyList();
            return View(newsClassifies);
        }
        [HttpGet]
        public JsonResult GetBanner()
        {
            return Json(_bannerService.GetBannerList());
        }

        [HttpGet]
        public JsonResult GetNewsTotal()
        {
            return Json(_newsService.GetNewsCount(c => true));
        }

        [HttpGet]
        public JsonResult GetHomeNews()
        {
            return Json(_newsService.GetNewsList(c => true, 6));
        }

        [HttpGet]
        public JsonResult GetCommentNews()
        {
            return Json(_newsService.GetNewCommentNewsList(c => true, 5));
        }

        [HttpGet]
        public JsonResult SearchOneNews(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
                return Json(new NewsPublish.Models.Response.ResponseModel
                {
                    Code = 0,
                    Result = "搜索关键字不能为空！"
                });
            return Json(_newsService.GetSearchOneNews(c => c.Title.Contains(keyWord)));
        }

        public IActionResult Wrong()
        {
            ViewData["Title"] = "404";
            return View(_newsService.GetNewsClassifyList());
        }
    }
}
