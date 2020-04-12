using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsPublish.Models.Response;
using NewsPublish.Service;

namespace NewsPublish.Web.Areas.Admin.Controllers
{
    public class NewsCommentController : AdminControllerBase
    {
        NewsCommentService _newsCommentService;

        public NewsCommentController(NewsCommentService newsCommentService)
        {
            _newsCommentService = newsCommentService;
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View(_newsCommentService.GetNewCommentList(c => true));
        }
        [HttpDelete]
        public JsonResult DeleteComment(int id)
        {
            if (id <= 0)
                return Json(new ResponseModel { Code = 0, Result = "参数错误！" });
            return Json(_newsCommentService.DeleteComment(id));
        }


    }
}