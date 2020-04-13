using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsPublish.Models.Request;
using NewsPublish.Models.Response;
using NewsPublish.Service;

namespace NewsPublish.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsService _newsService;
        private readonly NewsCommentService _newsCommentService;
        public NewsController(NewsService newsService, NewsCommentService newsCommentService)
        {
            _newsService = newsService;
            _newsCommentService = newsCommentService;
        }

        public IActionResult NewsClassify(int id)
        {
            if (id < 0) return RedirectToAction("Index", "Home");
            var classify = _newsService.GetOneNewsClassify(id);
            if (classify.Code == 0) return RedirectToAction("Index", "Home");
            ViewData["ClssifyName"] = classify.Data.Name;
            var newsList = _newsService.GetNewsList(c => c.NewsClassifyId == id, 6);
            ViewData["NewsList"] = newsList;
            var newCommentNews = _newsService.GetNewCommentNewsList(item => item.NewsClassifyId == id, 5);
            ViewData["NewCommentNews"] = newCommentNews;
            ViewData["Title"] = "首页";
            var newsClassifies = _newsService.GetNewsClassifyList();
            return View(newsClassifies);
        }

        public IActionResult NewsDetail(int id)
        {
            if (id < 0) return RedirectToAction("Index", "Home");

            var news = _newsService.GetNews(id);
            if (news.Code == 0) return RedirectToAction("Index", "Home");
            ViewData["News"] = news;
            ViewData["Title"] = $"{news.Data.Title}-详情页";
            var recommendNews = _newsService.GetRecommendNewsList(id);
            ViewData["RecommendNews"] = recommendNews;
            var newsCommens = _newsCommentService.GetNewCommentList(c => c.NewsId == id);
            ViewData["NewsComments"] = newsCommens;
            return View(_newsService.GetNewsClassifyList());
        }

        [HttpPost]
        public JsonResult AddComment(AddCommentDto dto)
        {
            if (dto.NewsId <= 0)
                return Json(new ResponseModel() { Code = 0, Result = "新闻不存在！" });
            if (string.IsNullOrWhiteSpace(dto.Contents))
                return Json(new ResponseModel() { Code = 0, Result = "评论内容不能为空！" });
            return Json(_newsCommentService.AddNewsComment(dto));
        }
    }
}