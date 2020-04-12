using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsPublish.Service;

namespace NewsPublish.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsService _newsService;
        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }


        public ActionResult NewsClassify(int id)
        {
            ViewData["Title"] = "首页";
            var newsClassifies = _newsSerrvice.GetNewsClassifyList();
            return View(newsClassifies);
        }
    }
}