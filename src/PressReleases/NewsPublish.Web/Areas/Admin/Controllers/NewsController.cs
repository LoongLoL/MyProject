using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using NewsPublish.Models.Entitys;
using NewsPublish.Models.Request;
using NewsPublish.Models.Response;
using NewsPublish.Service;

namespace NewsPublish.Web.Areas.Admin.Controllers
{
    public class NewsController : AdminControllerBase
    {
        NewsService _newsService;
        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        #region 新闻
        public ActionResult Index()
        {
            var newsClassify = _newsService.GetNewsClassifyList();
            return View(newsClassify);
        }

        [HttpGet]
        public JsonResult GetNewsPageList(int pageSize, int pageIndex, int classifyId, string keyWord)
        {
            List<Expression<Func<News, bool>>> wheres = new List<Expression<Func<News, bool>>>();
            if (classifyId > 0)
                wheres.Add(c => c.NewsClassifyId == classifyId);
            if (!string.IsNullOrEmpty(keyWord))
                wheres.Add(c => c.Title.Contains(keyWord));
            var news = _newsService.GetNewsListPage(pageSize, pageIndex, out var total, wheres);
            return Json(new { Total = total, Data = news.Data });
        }

        #endregion


        #region 新闻类别
        public ActionResult NewsClassify()
        {
            var newsClassify = _newsService.GetNewsClassifyList();
            return View(newsClassify);
        }

        public ActionResult AddClassify()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddClassify(AddNewsClassifyDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return Json(new ResponseModel() { Code = 0, Result = "类别名称不能为空！" });
            return Json(_newsService.AddNewsClassify(dto));
        }

        public ActionResult EditClassify(int id)
        {
            return View(_newsService.GetOneNewsClassify(id));
        }

        [HttpPut]
        public JsonResult EditClassify(UpdateNewsClassifyDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return Json(new ResponseModel() { Code = 0, Result = "类别名称不能为空！" });
            return Json(_newsService.UpdateNewsClassify(dto));
        }
        #endregion
    }
}